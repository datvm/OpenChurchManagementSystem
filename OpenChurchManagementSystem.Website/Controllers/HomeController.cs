using OpenChurchManagementSystem.Website.Framework;
using OpenChurchManagementSystem.Website.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenChurchManagementSystem.Website.Controllers
{

    [Authorize]
    public class HomeController : BaseChurchController
    {
        
        public ActionResult Index()
        {
            var model = new ChurchViewModel(this.ChurchDomain.Church);
            return this.View(model);
        }

    }
}