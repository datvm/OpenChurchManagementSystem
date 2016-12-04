using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OpenChurchManagementSystem.WebApi.Models;
using System;
using System.Web;
using System.Linq;
using System.Data.Entity;
using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Identities;
using SkyWeb.DatVM.Mvc.Autofac;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;

namespace OpenChurchManagementSystem.WebApi
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<IdentityAccount, int>
    {

        private AspNetIdentityUserStore IdentityUserStore
        {
            get
            {
                return this.Store as AspNetIdentityUserStore;
            }
        }

        public ApplicationUserManager(IUserStore<IdentityAccount, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(AspNetIdentityUserStore.Create());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<IdentityAccount, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityAccount, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override Task<IdentityAccount> FindAsync(string userName, string password)
        {
            throw new NotSupportedException("Please use the overload with tenant Id");
        }

        public async Task<IdentityAccount> FindAsync(string userName, string password, int? churchId)
        {
            var service = DependencyUtils.Resolve<IIdentityAccountService>();
            var user = await service.FindUsernameByChurch(userName, churchId);

            if (user == null)
            {
                return null;
            }

            var hashResult = this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);

            if (hashResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                await this.IdentityUserStore.SetPasswordHashAsync(user, this.PasswordHasher.HashPassword(password));
                hashResult = PasswordVerificationResult.Success;
            }

            if (hashResult == PasswordVerificationResult.Success)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

    }
}
