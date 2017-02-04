using Microsoft.AspNet.Identity.Owin;
using OpenChurchManagementSystem.WebApi.Framework;
using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Identities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenChurchManagementSystem.WebApi.Areas.SysAdmin.Controllers
{

    [Authorize]
    [RoutePrefix("api/v1/sysadmin")]
    public class AccountController : BaseChurchApiController
    {

        [AllowAnonymous]
        [HttpGet, Route("initialize")]
        public async Task<string> Initialize(string token)
        {
            
            if (string.IsNullOrEmpty(token) || ConfigurationManager.AppSettings["AccountInitializationToken"] != token)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Token"));
            }

            var result = "";

            var owinContext = System.Web.HttpContextExtensions.GetOwinContext(System.Web.HttpContext.Current);

            // Create account if not exist
            var userManager = owinContext.Get<ApplicationUserManager>();

            var adminEmail = ConfigurationManager.AppSettings["AccountInitializationAdminEmail"];
            var adminPassword = ConfigurationManager.AppSettings["AccountInitializationAdminPassword"];

            var adminAccount = await userManager.FindAsync(adminEmail, this.Church.Id);
            if (adminAccount == null)
            {
                // Create account
                adminAccount = new Models.Entities.IdentityAccount()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                await userManager.CreateAsync(adminAccount, adminPassword, this.Church.Id);
                result += $"Admin account not exist. Created {adminEmail}.{Environment.NewLine}";
            }
            else
            {
                // Reset password
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(adminAccount.Id);
                await userManager.ResetPasswordAsync(adminAccount.Id, resetToken, adminPassword);

                result += $"Admin account is already exist. Resetted password.{Environment.NewLine}";
            }

            // Initialize Roles
            var allRoles = Enum.GetValues(typeof(DbIdentityRole)).Cast<DbIdentityRole>();
            var roleManager = owinContext.Get<AspNetIdentityRoleManager>();

            foreach (var role in allRoles)
            {
                var roleName = role.ToString();
                var roleEntity = await roleManager.FindByNameAsync(roleName);

                if (roleEntity == null)
                {
                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Id = roleName,
                        Name = roleName,
                    });

                    result += $"Creating missing role {roleName}.{Environment.NewLine}";
                }
            }

            // Setting SysAdmin role
            var sysAdminRole = DbIdentityRole.SysAdmin.ToString();
            var isSysAdmin = await userManager.IsInRoleAsync(adminAccount.Id, sysAdminRole);
            if (!isSysAdmin)
            {
                await userManager.AddToRoleAsync(adminAccount.Id, sysAdminRole);
                result += $"Adding {adminEmail} account to SysAdmin role.{Environment.NewLine}";
            }

            return result;
        }

    }
}
