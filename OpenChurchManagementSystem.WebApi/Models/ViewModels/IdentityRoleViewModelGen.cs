//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenChurchManagementSystem.WebApi.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class IdentityRoleViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<OpenChurchManagementSystem.WebApi.Models.Entities.IdentityRole>
    {
    	
    			public virtual string Id { get; set; }
    			public virtual string Name { get; set; }
    	
    	public IdentityRoleViewModel() : base() { }
    	public IdentityRoleViewModel(OpenChurchManagementSystem.WebApi.Models.Entities.IdentityRole entity) : base(entity) { }
    
    }
}
