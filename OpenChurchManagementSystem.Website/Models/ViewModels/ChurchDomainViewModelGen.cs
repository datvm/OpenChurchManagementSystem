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
    
    public partial class ChurchDomainViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<OpenChurchManagementSystem.Website.Models.Entities.ChurchDomain>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual int ChurchId { get; set; }
    			public virtual string Hostname { get; set; }
    			public virtual int Port { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public ChurchDomainViewModel() : base() { }
    	public ChurchDomainViewModel(OpenChurchManagementSystem.Website.Models.Entities.ChurchDomain entity) : base(entity) { }
    
    }
}
