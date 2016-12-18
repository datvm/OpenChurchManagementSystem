using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{

    public partial interface IIdentityAccountService
    {
        Task SetPasswordHashAsync(IdentityAccount account, string passwordHash);
        Task SetSecurityStampAsync(IdentityAccount account, string stamp);
        Task<IdentityAccount> FindUsernameByChurch(string username, int? churchId);
        bool IsOfChurch(int accountId, int churchId);
    }

    public partial class IdentityAccountService
    {

        public async Task SetPasswordHashAsync(IdentityAccount account, string passwordHash)
        {
            account.PasswordHash = passwordHash;
            await this.UpdateAsync(account);
        }

        public async Task SetSecurityStampAsync(IdentityAccount account, string stamp)
        {
            account.SecurityStamp = stamp;
            await this.UpdateAsync(account);
        }

        public async Task<IdentityAccount> FindUsernameByChurch(string username, int? churchId)
        {
            return await this.FirstOrDefaultActiveAsync(q => q.ChurchId == churchId && q.UserName == username);
        }

        public bool IsOfChurch(int accountId, int churchId)
        {
            return this.FirstOrDefaultActive(q => q.Id == accountId && q.ChurchId == churchId) != null;
        }

        protected override void OnCreate(IdentityAccount entity)
        {
            // Validate
            var duplicate = this.FirstOrDefaultActiveAsync(q => q.UserName == entity.UserName && q.ChurchId == entity.ChurchId);
            if (duplicate != null)
            {
                throw new ArgumentException("Account already exist");
            }

            base.OnCreate(entity);
        }

    }

}