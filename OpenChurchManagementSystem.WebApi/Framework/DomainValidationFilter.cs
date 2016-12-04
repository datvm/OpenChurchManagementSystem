using OpenChurchManagementSystem.WebApi.Models.Identities;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OpenChurchManagementSystem.WebApi.Framework
{

    public class DomainValidationFilter : ActionFilterAttribute
    {

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (this.Resolve<IdentityChurch>() == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Invalid Hostname",
                };
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

    }

}