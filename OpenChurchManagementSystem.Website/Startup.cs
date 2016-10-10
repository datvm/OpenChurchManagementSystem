using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OpenChurchManagementSystem.Website.Startup))]
namespace OpenChurchManagementSystem.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
