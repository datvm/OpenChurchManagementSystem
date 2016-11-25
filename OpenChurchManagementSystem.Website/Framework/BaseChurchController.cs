using SkyWeb.DatVM.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using OpenChurchManagementSystem.Website.Models.Entities;
using OpenChurchManagementSystem.Website.Models.Entities.Services;
using OpenChurchManagementSystem.Website.Models.ViewModels;
using SkyWeb.DatVM.WebApi;
using System.Web.Http.Controllers;
using System.Net.Http;

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
            this.ViewBag.ChurchDomainInfo = new ChurchDomainViewModel(this.ChurchDomain);
            this.ViewBag.ChurchInfo = new ChurchViewModel(this.ChurchDomain.Church);

            base.OnActionExecuting(filterContext);
        }

    }

    public class BaseChurchApiController : BaseApiController
    {

        public int ChurchId { get; private set; }
        public ChurchDomain ChurchDomain { get; private set; }
        public string ResolvedIP { get; private set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            var request = controllerContext.Request;

            this.ResolvedIP = request.Headers.Contains("CF-Connecting-IP") ?
                request.Headers.GetValues("CF-Connecting-IP").First() : 
                this.GetClientIp();

            var uri = controllerContext.Request.RequestUri;
            this.ChurchDomain = this.Service<IChurchDomainService>()
                .FindDomain(request.RequestUri.Host, request.RequestUri.Port);
            
            base.Initialize(controllerContext);
        }

        private string GetClientIp()
        {
            return HttpContext.Current?.Request?.UserHostAddress;
        }

    }

}