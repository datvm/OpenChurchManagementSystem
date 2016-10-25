using OpenChurchManagementSystem.Website.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenChurchManagementSystem.Website.Areas.Admin.Controllers
{

    [Authorize(Roles = "SysAdmin,Admin")]
    public class AccountController : BaseChurchController
    {
        
    }

}