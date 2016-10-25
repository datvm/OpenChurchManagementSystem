using OpenChurchManagementSystem.Website.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenChurchManagementSystem.Website.Areas.Admin.Controllers
{

    [RoutePrefix("Admin")]
    public class HomeController : BaseChurchController
    {

        [Route("*parameters")]   
        public ActionResult Index(string[] parameters)
        {
            return this.View();
        }

    }
}