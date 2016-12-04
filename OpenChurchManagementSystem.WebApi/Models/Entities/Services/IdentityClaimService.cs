using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{

    public partial interface IIdentityClaimService
    {

        IQueryable<IdentityClaim> GetByUser(int userId);
        Task CreateAsync(int userId, string claimType, string claimValue);
        Task DeactivateAsync(int userId, string claimType, string claimValue);

    }

    public partial class IdentityClaimService
    {

        public IQueryable<IdentityClaim> GetByUser(int userId)
        {
            return this.GetActive(q => q.UserId == userId);
        }

        public async Task CreateAsync(int userId, string claimType, string claimValue)
        {
            await this.CreateAsync(new IdentityClaim()
            {
                UserId = userId,
                ClaimType = claimType,
                ClaimValue = claimValue,
            });
        }

        public async Task DeactivateAsync(int userId, string claimType, string claimValue)
        {
            var claim = await this.FirstOrDefaultActiveAsync(q => q.UserId == userId && q.ClaimType == claimType && q.ClaimValue == claimValue);

            if (claim != null)
            {
                await this.DeactivateAsync(claim);
            }
        }

    }

}