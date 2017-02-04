using System.Web.Mvc;

namespace OpenChurchManagementSystem.WebApi.Areas.SysAdmin
{
    public class SysAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SysAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
        }
    }
}