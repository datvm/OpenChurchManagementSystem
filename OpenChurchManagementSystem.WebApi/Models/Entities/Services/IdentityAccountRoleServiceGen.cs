//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IIdentityAccountRoleService : SkyWeb.DatVM.Data.IBaseService<IdentityAccountRole>
    {
    }
    
    public partial class IdentityAccountRoleService : SkyWeb.DatVM.Data.BaseService<IdentityAccountRole>, IIdentityAccountRoleService
    {
        public IdentityAccountRoleService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IIdentityAccountRoleRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
