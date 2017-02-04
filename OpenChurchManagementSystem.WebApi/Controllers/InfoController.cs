using OpenChurchManagementSystem.WebApi.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenChurchManagementSystem.WebApi.Controllers
{

    [Authorize]
    [RoutePrefix("api/v1/info")]
    public class InfoController : BaseChurchApiController
    {
        
        [AllowAnonymous]
        [HttpGet, System.Web.Http.Route("ChurchName")]
        public string ChurchName()
        {
            if (this.Church == null)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Hostname"));
            }

            return this.Church.Name;
        }
        
        [AllowAnonymous]
        [HttpGet, System.Web.Http.Route("UserInfo")]
        public string UserInfo()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No authentication info found"));
            }

            return this.User.Identity.Name;
        }

        [HttpGet, Route("Authentication")]
        public void ValidateAuthentication() { }

    }

}
