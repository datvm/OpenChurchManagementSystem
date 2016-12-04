using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{

    public partial interface IIdentityRoleService
    {

        Task<IdentityRole> FindByIdAsync(string id);
        Task<IdentityRole> FindByNameAsync(string name);
        
    }

    public partial class IdentityRoleService
    {

        public async Task<IdentityRole> FindByIdAsync(string id)
        {
            return await this.GetActiveAsync(id);
        }

        public async Task<IdentityRole> FindByNameAsync(string name)
        {
            return await this.FirstOrDefaultActiveAsync(q => q.Name == name);
        }

    }
}