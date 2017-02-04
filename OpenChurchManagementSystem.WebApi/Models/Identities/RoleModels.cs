using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using OcmsEntities = OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace OpenChurchManagementSystem.WebApi.Models.Entities
{

    public partial class IdentityRole : IRole<string>
    {

        public const string AdminRoles = "SysAdmin,Admin";

    }

    public enum DbIdentityRole
    {
        SysAdmin,
        Admin,

    }

}

namespace OpenChurchManagementSystem.WebApi.Models.Identities
{
    
    public class AspNetIdentityRoleManager : RoleManager<OcmsEntities.IdentityRole, string>
    {

        public AspNetIdentityRoleManager(IRoleStore<OcmsEntities.IdentityRole, string> store) : base(store) { }

        public static AspNetIdentityRoleManager Create(IdentityFactoryOptions<AspNetIdentityRoleManager> options, IOwinContext context)
        {
            var roleStore = new AspNetIdentityRoleStore();
            return new AspNetIdentityRoleManager(roleStore);
        }

    }

    public class AspNetIdentityRoleStore : IRoleStore<OcmsEntities.IdentityRole>
    {

        private IIdentityRoleService roleServiceField = null;

        private IIdentityRoleService RoleService
        {
            get
            {
                if (this.roleServiceField == null)
                {
                    this.roleServiceField = DependencyUtils.Resolve<IIdentityRoleService>();
                }

                return this.roleServiceField;
            }
        }

        public static AspNetIdentityRoleStore Create()
        {
            return new AspNetIdentityRoleStore();
        }

        public async Task CreateAsync(OcmsEntities.IdentityRole role)
        {
            await this.RoleService.CreateAsync(role);
        }

        public async Task DeleteAsync(OcmsEntities.IdentityRole role)
        {
            await this.RoleService.DeleteAsync(role);
        }

        public async Task<OcmsEntities.IdentityRole> FindByIdAsync(string roleId)
        {
            return await this.RoleService.FindByIdAsync(roleId);
        }

        public async Task<OcmsEntities.IdentityRole> FindByNameAsync(string roleName)
        {
            return await this.RoleService.FindByNameAsync(roleName);
        }

        public async Task UpdateAsync(OcmsEntities.IdentityRole role)
        {
            await this.RoleService.UpdateAsync(role);
        }

        public void Dispose()
        {
        }

    }

}