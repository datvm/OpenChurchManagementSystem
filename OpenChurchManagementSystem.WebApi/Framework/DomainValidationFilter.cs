using Microsoft.AspNet.Identity;
using OpenChurchManagementSystem.WebApi.Models.Entities.Services;
using OpenChurchManagementSystem.WebApi.Models.Identities;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OpenChurchManagementSystem.WebApi.Framework
{

    public class DomainValidationFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var identityChurch = this.Resolve<IdentityChurch>();
            if (identityChurch == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Invalid Hostname"); ;
            }

            // Validate if the logged in user is from correct church
            var principal = actionContext.ControllerContext.RequestContext.Principal;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                var userId = int.Parse(principal.Identity.GetUserId());
                // Validate against the church Id
                var accountService = this.Resolve<IIdentityAccountService>();

                if (!accountService.IsOfChurch(userId, identityChurch.Church.Id))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Invalid Token"); 
                }
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

    }

}