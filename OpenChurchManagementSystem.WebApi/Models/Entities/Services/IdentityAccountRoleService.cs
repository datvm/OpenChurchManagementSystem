using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{

    public partial interface IIdentityAccountRoleService
    {

        Task AddToRole(int userId, string roleId);
        Task RemoveFromRole(int userId, string roleId);
        IQueryable<IdentityRole> GetAllRoles(int userId);
        Task<bool> IsInRole(int userId, string roleId);

    }

    public partial class IdentityAccountRoleService
    {

        public async Task AddToRole(int userId, string roleId)
        {
            // Check if exist
            if (await this.FirstOrDefaultActiveAsync(q => q.IdentityAccountId == userId && q.IdentityRoleId == roleId) != null)
            {
                return;
            }

            // Reactive if one exist
            var entity = await this.FirstOrDefaultAsync(q => q.IdentityAccountId == userId && q.IdentityRoleId == roleId);
            if (entity != null)
            {
                entity.Active = true;
                await this.SaveAsync();
            }
            else
            {
                entity = new IdentityAccountRole()
                {
                    IdentityAccountId = userId,
                    IdentityRoleId = roleId,
                };

                await this.CreateAsync(entity);
            }
        }

        public async Task RemoveFromRole(int userId, string roleId)
        {
            var roleMappings = this.GetActive(q => q.IdentityAccountId == userId && q.IdentityRoleId == roleId);

            foreach (var roleMapping in roleMappings)
            {
                roleMapping.Active = false;
            }

            await this.SaveAsync();
        }

        public IQueryable<IdentityRole> GetAllRoles(int userId)
        {
            return this.GetActive(q => q.IdentityAccountId == userId)
                .Select(q => q.IdentityRole);
        }

        public async Task<bool> IsInRole(int userId, string roleId)
        {
            return (await this.FirstOrDefaultActiveAsync(q => q.IdentityAccountId == userId && q.IdentityRoleId == roleId)) != null;
        }

    }
}