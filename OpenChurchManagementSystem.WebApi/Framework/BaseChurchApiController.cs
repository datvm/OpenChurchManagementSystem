using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
using OpenChurchManagementSystem.WebApi.Models.Identities;
using OpenChurchManagementSystem.WebApi.Models.ViewModels;
using SkyWeb.DatVM.Mvc.Autofac;
using SkyWeb.DatVM.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OpenChurchManagementSystem.WebApi.Framework
{
    public class BaseChurchApiController : BaseApiController
    {
        
        public ChurchViewModel Church { get; private set; }
        public ChurchDomainViewModel ChurchDomain { get; private set; }
        public string ResolvedIP { get; private set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            var request = controllerContext.Request;

            this.ResolvedIP = request.Headers.Contains("CF-Connecting-IP") ?
                request.Headers.GetValues("CF-Connecting-IP").First() :
                this.GetClientIp();

            var identityChurch = DependencyUtils.Resolve<IdentityChurch>();
            
            this.Church =  identityChurch.Church;
            this.ChurchDomain = identityChurch.ChurchDomain;

            base.Initialize(controllerContext);
        }

        private string GetClientIp()
        {
            return System.Web.HttpContext.Current?.Request?.UserHostAddress;
        }

    }
}
