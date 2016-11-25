using OpenChurchManagementSystem.Website.Framework;
using OpenChurchManagementSystem.Website.Models.Entities.Services;
using OpenChurchManagementSystem.Website.Models.Identities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenChurchManagementSystem.Website.Areas.Admin.Controllers
{

    [RoutePrefix("api/v1/account")]
    [Authorize(Roles = IdentityRoles.AccountManagement)]
    public class AccountController : BaseChurchApiController
    {

        [AllowAnonymous]
        [HttpGet, Route("authorized"), Route("ping")]
        public IHttpActionResult Authorized()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Ok(new
                {
                    Username = this.User.Identity.Name,
                });
            }
            else
            {
                return this.Unauthorized();
            }
        }

        public async Task<IHttpActionResult> Accounts()
        {
            var service = this.Service<IAspNetUserService>();

            var list = await service.GetActive()
                .ToListAsync();

            return this.Ok(list);
        }

    }

}
