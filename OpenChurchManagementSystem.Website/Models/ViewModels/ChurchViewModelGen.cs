//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenChurchManagementSystem.Website.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChurchViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<OpenChurchManagementSystem.Website.Models.Entities.Church>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string Name { get; set; }
    			public virtual string Description { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public ChurchViewModel() : base() { }
    	public ChurchViewModel(OpenChurchManagementSystem.Website.Models.Entities.Church entity) : base(entity) { }
    
    }
}