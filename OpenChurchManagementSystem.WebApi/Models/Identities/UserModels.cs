using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System.Security.Claims;
using System.Data.Entity;

namespace OpenChurchManagementSystem.WebApi.Models.Entities
{

    public partial class IdentityAccount : IUser<int>
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager, string authenticationType)
        {
            var result = await userManager.CreateIdentityAsync(this, authenticationType);

            result.AddClaim(new Claim("ChurchId", this.ChurchId.ToString()));

            return result;
        }

    }

}

namespace OpenChurchManagementSystem.WebApi.Models.Identities
{

    public class AspNetIdentityUserStore :
        IUserStore<IdentityAccount, int>,
        IUserClaimStore<IdentityAccount, int>,
        IUserPasswordStore<IdentityAccount, int>,
        IUserSecurityStampStore<IdentityAccount, int>
    {
        private object locker = new object();
        
        private IIdentityAccountService AccountService { get; set; }

        private IIdentityClaimService ClaimService { get; set; }

        public AspNetIdentityUserStore(IIdentityAccountService accountService, IIdentityClaimService claimService)
        {
            this.AccountService = accountService;
            this.ClaimService = claimService;
        }

        public static AspNetIdentityUserStore Create()
        {
            return new AspNetIdentityUserStore(
                DependencyUtils.Resolve<IIdentityAccountService>(),
                DependencyUtils.Resolve<IIdentityClaimService>());
        }

        #region IUserStore

        public async Task CreateAsync(IdentityAccount user)
        {
            await this.AccountService.CreateAsync(user);
        }

        public async Task DeleteAsync(IdentityAccount user)
        {
            await this.AccountService.DeactivateAsync(user);
        }

        public async Task<IdentityAccount> FindByIdAsync(int userId)
        {
            return await this.AccountService.FirstOrDefaultActiveAsync(q => q.Id == userId);
        }

        public async Task<IdentityAccount> FindByNameAsync(string userName)
        {
            return await this.AccountService.FirstOrDefaultActiveAsync(q => q.UserName == userName);
        }

        public async Task UpdateAsync(IdentityAccount user)
        {
            await this.AccountService.UpdateAsync(user);
        }

        #endregion

        #region IUserClaimStore

        public async Task<IList<Claim>> GetClaimsAsync(IdentityAccount user)
        {
            return (await this.ClaimService
                .GetByUser(user.Id)
                .ToListAsync())
                .Select(q => new Claim(q.ClaimType, q.ClaimValue))
                .ToList();
        }

        public async Task AddClaimAsync(IdentityAccount user, Claim claim)
        {
            await this.ClaimService.CreateAsync(user.Id, claim.Type, claim.Value);
        }

        public async Task RemoveClaimAsync(IdentityAccount user, Claim claim)
        {
            await this.ClaimService.DeactivateAsync(user.Id, claim.Type, claim.Value);
        }

        #endregion

        #region IUserPasswordStore

        public async Task SetPasswordHashAsync(IdentityAccount user, string passwordHash)
        {
            await this.AccountService.SetPasswordHashAsync(user, passwordHash);
        }

        public Task<string> GetPasswordHashAsync(IdentityAccount user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityAccount user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion

        #region IUserSecurityStampStore

        public async Task SetSecurityStampAsync(IdentityAccount user, string stamp)
        {
            await this.AccountService.SetSecurityStampAsync(user, stamp);
        }

        public Task<string> GetSecurityStampAsync(IdentityAccount user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        #endregion

        public void Dispose()
        {
        }

    }

}