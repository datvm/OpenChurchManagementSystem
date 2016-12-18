using Microsoft.AspNet.Identity.Owin;
using OpenChurchManagementSystem.WebApi.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OpenChurchManagementSystem.WebApi.Areas.SysAdmin.Controllers
{

    [Authorize(Roles = "SysAdmin")]
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

            // Create account if not exist
            var userManager = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();

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
                result += $"Admin account not exist. Created {adminEmail}.";
            }
            else
            {
                // Reset password
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(adminAccount.Id);
                await userManager.ResetPasswordAsync(adminAccount.Id, resetToken, adminPassword);

                result += "Admin account is already exist. Resetted password.";
            }

            // Initialize Roles

            return result;
        }



    }

}
