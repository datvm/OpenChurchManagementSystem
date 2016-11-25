﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenChurchManagementSystem.Website.Models.Identities
{

    public enum DbIdentityRoles
    {

        SysAdmin,
        Admin,
        
    }

    public static class IdentityRoles
    {
        public const string AccountManagement = "SysAdmin,Admin";
    }

}