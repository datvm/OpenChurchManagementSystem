using OpenChurchManagementSystem.Website.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OpenChurchManagementSystem.Website.Areas.Admin.Controllers
{

    [RoutePrefix("Admin")]
    public class HomeController : BaseChurchController
    {

        [AllowAnonymous]
        [Route("{*parameters}")]
        public ActionResult Index(string[] parameters)
        {
            return this.View();
        }

        public ActionResult Authenticated()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }

    }
}