using SkyWeb.DatVM.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using OpenChurchManagementSystem.Website.Models.Entities;
using OpenChurchManagementSystem.Website.Models.Entities.Services;

namespace OpenChurchManagementSystem.Website.Framework
{

    public class BaseChurchController : BaseController
    {

        public int ChurchId { get; private set; }
        public ChurchDomain ChurchDomain { get; private set; }
        public string ResolvedIP { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ResolvedIP = this.Request.Headers["CF-Connecting-IP"] ?? this.Request.UserHostAddress;

            this.ChurchDomain = this.Service<IChurchDomainService>()
                .FindDomain(this.Request.Url.Host, this.Request.Url.Port);

            base.OnActionExecuting(filterContext);
        }

    }

}