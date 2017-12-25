using System.Web;
using System.Web.Optimization;

namespace QuanLyXeKhach
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Assets/datvecss").Include(
                    "~/assets/AssetsDatve/vendor/bootstrap/css/bootstrap.min.css",
                    "~/assets/AssetsDatve/vendor/metisMenu/metisMenu.min.css",
                    "~/assets/AssetsDatve/dist/css/sb-admin-2.css",
                    "~/assets/AssetsDatve/vendor/morrisjs/morris.css",
                    "~/assets/AssetsDatve/vendor/font-awesome/css/font-awesome.min.css",
                    "~/assets/AssetsDatve/vendor/datatables-plugins/dataTables.bootstrap.css",
                    "~/assets/AssetsDatve/vendor/datatables-responsive/dataTables.responsive.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datvejs").Include(
                        "~/assets/AssetsDatve/vendor/flot/excanvas.min.js",
                        "~/assets/AssetsDatve/vendor/flot/excanvas.min.js",
                        "~/assets/AssetsDatve/vendor/flot/jquery.flot.pie.js",
                        "~/assets/AssetsDatve/vendor/flot/jquery.flot.resize.js",
                        "~/assets/AssetsDatve/vendor/flot/jquery.flot.time.js",
                        "~/assets/AssetsDatve/vendor/flot-tooltip/jquery.flot.tooltip.min.js",
                        "~/assets/AssetsDatve/data/flot-data.js",
                        "~/assets/AssetsDatve/dist/js/sb-admin-2.js",
                        "~/assets/AssetsDatve/vendor/datatables/js/jquery.dataTables.min.js",
                        "~/assets/AssetsDatve/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                        "~/assets/AssetsDatve/vendor/datatables-responsive/dataTables.responsive.js"
                        ));
        }
    }
}
