using OpenChurchManagementSystem.WebApi.Models.Entities;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
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
            return System.Web.HttpContext.Current?.Request?.UserHostAddress;
        }

    }
}
