//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenChurchManagementSystem.WebApi.Models.Entities.Repositories
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IChurchRepository : SkyWeb.DatVM.Data.IBaseRepository<Church>
    {
    }
    
    public partial class ChurchRepository : SkyWeb.DatVM.Data.BaseRepository<Church>, IChurchRepository
    {
    	public ChurchRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}