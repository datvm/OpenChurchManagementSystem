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

    }

}