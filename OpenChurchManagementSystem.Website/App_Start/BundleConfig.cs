using System.Web;
using System.Web.Optimization;

namespace OpenChurchManagementSystem.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // JQuery
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            // Modernizr
            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            // Material Kit & Dashboard
            bundles.Add(new ScriptBundle("~/scripts/materialkit").Include(
                      "~/Scripts/MaterialKit/material.min.js",
                      "~/Scripts/MaterialKit/bootstrap-datepicker.js",
                      "~/Scripts/MaterialKit/nouislider.min.js",
                      "~/Scripts/MaterialKit/material-kit.js"));
            bundles.Add(new StyleBundle("~/styles/materialkit").Include(
                      "~/Content/MaterialKit/material-kit.css"));

            bundles.Add(new ScriptBundle("~/scripts/materialdashboard").Include(
                      "~/Scripts/MaterialDashboard/material.min.js",
                      "~/Scripts/MaterialKit/material-dashboard.js"));
            bundles.Add(new StyleBundle("~/styles/materialdashboard").Include(
                      "~/Content/MaterialDashboard/material-dashboard.css"));

            // Site
            bundles.Add(new StyleBundle("~/styles/site")
                .Include("~/Content/Site.css"));

        }
    }
}
