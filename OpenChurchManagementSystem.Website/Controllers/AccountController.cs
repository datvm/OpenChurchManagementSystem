using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OpenChurchManagementSystem.Website.Models;
using System.Configuration;
using System.Text;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using OpenChurchManagementSystem.Website.Models.ViewModels;
using System.Collections.Generic;
using OpenChurchManagementSystem.Website.Models.Identities;
using OpenChurchManagementSystem.Website.Framework;

namespace OpenChurchManagementSystem.Website.Controllers
{

#if !DEBUG // Require HTTPS on build
    [RequireHttps]
#endif
    [Authorize]
    public class AccountController : BaseChurchController
    {
        private ApplicationDbContext _dbContext;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        
        public ApplicationDbContext DbContext
        {
            get
            {
                if (this._dbContext == null)
                {
                    this._dbContext = new ApplicationDbContext();
                }

                return this._dbContext;
            }
            private set { this._dbContext = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                if (this._signInManager == null)
                {
                    this._signInManager = new ApplicationSignInManager(this.UserManager, this.HttpContext.GetOwinContext().Authentication);
                }

                return this._signInManager;
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (this._userManager == null)
                {
                    var userStore = new ApplicationUserStore<ApplicationUser>(this.DbContext) { TenantId = this.ChurchId, };
                    this._userManager = new ApplicationUserManager(userStore);
                }

                return this._userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (this._roleManager == null)
                {
                    var roleStore = new RoleStore<IdentityRole>(this.DbContext);
                    this._roleManager = new ApplicationRoleManager(roleStore);
                }

                return this._roleManager;
            }
            private set { this._roleManager = value; }
        }

        [AllowAnonymous]
        public async Task<ActionResult> Initialize(string token)
        {
            // Prevent empty token from re-initializing
            if (string.IsNullOrEmpty(token) || token != ConfigurationManager.AppSettings["AccountInitializationToken"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var adminEmail = ConfigurationManager.AppSettings["AccountInitializationAdminEmail"];
            var adminPassword = ConfigurationManager.AppSettings["AccountInitializationAdminPassword"];

            var result = new StringBuilder();

            #region Admin Account Creating/Resetting

            var adminAccount = await this.UserManager.FindByNameAsync(adminEmail);

            if (adminAccount == null)
            {
                adminAccount = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                var operationResult = await this.UserManager.CreateAsync(adminAccount, adminPassword);

                if (operationResult.Succeeded)
                {
                    result.AppendLine("Admin account created.");
                }
                else
                {
                    result.AppendLine($"Admin account creation failed: {string.Join(Environment.NewLine, operationResult.Errors)}.");
                    goto RETURN_POINT;
                }
            }
            else
            {
                // Reset password
                var resetToken = await this.UserManager.GeneratePasswordResetTokenAsync(adminAccount.Id);
                var operationResult = await this.UserManager.ResetPasswordAsync(adminAccount.Id, resetToken, adminPassword);

                if (operationResult.Succeeded)
                {
                    result.AppendLine("Admin password reseted.");
                }
                else
                {
                    result.AppendLine($"Admin password reset failed: {string.Join(Environment.NewLine, operationResult.Errors)}.");
                    goto RETURN_POINT;
                }
            }

            #endregion

            #region SysAdmin Role

            var roles = await this.EnsureRoles(result);
            if (roles == null)
            {
                goto RETURN_POINT;
            }

            var sysAdminRoleName = DbIdentityRoles.SysAdmin.ToString();
            if (!await this.UserManager.IsInRoleAsync(adminAccount.Id, sysAdminRoleName))
            {
                var operationResult = await this.UserManager.AddToRoleAsync(adminAccount.Id, sysAdminRoleName);

                if (operationResult.Succeeded)
                {
                    result.AppendLine("Added admin account to SysAdmin role.");
                }
                else
                {
                    result.AppendLine($"Adding Admin account to SysAdmin role failed: {string.Join(Environment.NewLine, operationResult.Errors)}.");
                    goto RETURN_POINT;
                }
            }
            
            #endregion

            RETURN_POINT:
            return this.Content(result.ToString());
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LogInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private async Task<Dictionary<string, IdentityRole>> EnsureRoles(StringBuilder report)
        {
            var result = new Dictionary<string, IdentityRole>();

            var allRoleNames = Enum.GetNames(typeof(DbIdentityRoles));
            var roleManager = this.RoleManager;

            foreach (var roleName in allRoleNames)
            {
                var role = await roleManager.FindByIdAsync(roleName);

                if (role == null)
                {
                    role = new IdentityRole()
                    {
                        Id = roleName,
                        Name = roleName,
                    };

                    var operationResult = await roleManager.CreateAsync(role);

                    if (operationResult.Succeeded)
                    {
                        report.AppendLine($"{roleName} role created.");
                    }
                    else
                    {
                        report.AppendLine($"{roleName} role creation failed: {string.Join(Environment.NewLine, operationResult.Errors)}.");
                        return null;
                    }
                }
                else
                {
                    if (role.Name != role.Id)
                    {
                        role.Name = role.Id;
                        var operationResult = await roleManager.UpdateAsync(role);

                        if (operationResult.Succeeded)
                        {
                            report.AppendLine($"{roleName} role name synced.");
                        }
                        else
                        {
                            report.AppendLine($"{roleName} role sync failed: {string.Join(Environment.NewLine, operationResult.Errors)}.");
                            return null;
                        }
                    }
                }

                result.Add(roleName, role);
            }

            return result;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}