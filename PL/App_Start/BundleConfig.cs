using System.Web;
using System.Web.Optimization;

namespace PL
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/material").IncludeDirectory(
            //            "~/Scripts/mdb", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/material").Include(
                "~/Sripts/mdb/arrive.min.js",
                "~/Sripts/mdb/bootstrap-datetimepicker.min.js",
                "~/Sripts/mdb/bootstrap-material-design.min.js",
                "~/Sripts/mdb/bootstrap-notify.js",
                "~/Sripts/mdb/bootstrap-selectpicker.js",
                "~/Sripts/mdb/mdb\bootstrap-tagsinput.js",
                "~/Sripts/mdb/jasny-bootstrap.min.js",
                "~/Sripts/mdb/jquery.bootstrap-wizard.js",
                "~/Sripts/mdb/jquery.dataTables.min.js",
                "~/Sripts/mdb/jquery.min.js",
                "~/Sripts/mdb/material-dashboard.js",
                "~/Sripts/mdb/moment.min.js",
                "~/Sripts/mdb/nouislider.min.js",
                "~/Sripts/mdb/perfect-scrollbar.jquery.min.js",
                "~/Sripts/mdb/popper.min.js"
                ));

            BundleTable.EnableOptimizations = true;


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

        }
    }
}
