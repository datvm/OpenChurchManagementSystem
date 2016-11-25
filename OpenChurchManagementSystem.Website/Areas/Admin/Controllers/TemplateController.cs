using OpenChurchManagementSystem.Website.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenChurchManagementSystem.Website.Areas.Admin.Controllers
{

    [RoutePrefix("Admin/Template")]
    public class TemplateController : BaseChurchController
    {
        
        public ActionResult PageHome()
        {
            return this.PartialView();
        }

        public ActionResult PageAccount()
        {
            return this.PartialView();
        }

        public ActionResult Shell()
        {
            return this.PartialView();
        }

        public ActionResult Sidebar()
        {
            return this.PartialView();
        }

        public ActionResult AccountPanel()
        {
            return this.PartialView();
        }

    }
}