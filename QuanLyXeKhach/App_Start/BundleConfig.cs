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


            //NhanSu
            bundles.Add(new StyleBundle("~/Content/NhanSu/css/side-bar").Include(
                      "~/Content/NhanSu/side-bar.css"));

            bundles.Add(new StyleBundle("~/Content/NhanSu/css/tab-content").Include(
                      "~/Content/NhanSu/tab-content.css"));

            bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/tab-content").Include(
                      "~/Scripts/NhanSu/tab-content.js"));


            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery-1.10.2.min").Include(
            //          "~/Scripts/NhanSu/jquery-1.10.2.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery-migrate-1.2.1.min").Include(
            //          "~/Scripts/NhanSu/jquery-migrate-1.2.1.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery-ui").Include(
            //          "~/Scripts/NhanSu/jquery-ui.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/bootstrap.min").Include(
            //          "~/Scripts/NhanSu/bootstrap.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/bootstrap-hover-dropdown").Include(
            //          "~/Scripts/NhanSu/bootstrap-hover-dropdown.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/html5shiv").Include(
            //          "~/Scripts/NhanSu/html5shiv.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/respond.min").Include(
            //          "~/Scripts/NhanSu/respond.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.metisMenu").Include(
            //          "~/Scripts/NhanSu/jquery.metisMenu.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.slimscroll").Include(
            //          "~/Scripts/NhanSu/jquery.slimscroll.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.cookie").Include(
            //          "~/Scripts/NhanSu/jquery.cookie.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/icheck.min").Include(
            //          "~/Scripts/NhanSu/icheck.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/custom.min").Include(
            //          "~/Scripts/NhanSu/custom.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.news-ticker").Include(
            //          "~/Scripts/NhanSu/jquery.news-ticker.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.menu").Include(
            //          "~/Scripts/NhanSu/jquery.menu.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/pace.min").Include(
            //          "~/Scripts/NhanSu/pace.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/holder").Include(
            //          "~/Scripts/NhanSu/holder.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/responsive-tabs").Include(
            //          "~/Scripts/NhanSu/responsive-tabs.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot").Include(
            //          "~/Scripts/NhanSu/jquery.flot.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.categories").Include(
            //          "~/Scripts/NhanSu/jquery.flot.categories.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.pie").Include(
            //          "~/Scripts/NhanSu/jquery.flot.pie.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.tooltip").Include(
            //          "~/Scripts/NhanSu/jquery.flot.tooltip.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.resize").Include(
            //          "~/Scripts/NhanSu/jquery.flot.resize.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.fillbetween").Include(
            //          "~/Scripts/NhanSu/jquery.flot.fillbetween.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.stack").Include(
            //          "~/Scripts/NhanSu/jquery.flot.stack.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/jquery.flot.spline").Include(
            //          "~/Scripts/NhanSu/jquery.flot.spline.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/zabuto_calendar.min").Include(
            //          "~/Scripts/NhanSu/zabuto_calendar.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/index").Include(
            //          "~/Scripts/NhanSu/index.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/highcharts").Include(
            //          "~/Scripts/NhanSu/highcharts.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/data").Include(
            //          "~/Scripts/NhanSu/.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/drilldown").Include(
            //          "~/Scripts/NhanSu/drilldown.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/exporting").Include(
            //          "~/Scripts/NhanSu/exporting.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/highcharts-more").Include(
            //          "~/Scripts/NhanSu/highcharts-more.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/charts-highchart-pie").Include(
            //          "~/Scripts/NhanSu/charts-highchart-pie.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/charts-highchart-more").Include(
            //          "~/Scripts/NhanSu/charts-highchart-more.js"));

            //bundles.Add(new ScriptBundle("~/bundles/NhanSu/Scripts/main").Include(
            //          "~/Scripts/NhanSu/main.js"));



            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/all").Include(
            //          "~/Content/NhanSu/all.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/animate").Include(
            //          "~/Content/NhanSu/animate.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/bootstrap.min").Include(
            //          "~/Content/NhanSu/bootstrap.min.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/font-awesome.min").Include(
            //          "~/Content/NhanSu/font-awesome.min.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/introjs").Include(
            //          "~/Content/NhanSu/introjs.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/jplist-custom").Include(
            //          "~/Content/NhanSu/jplist-custom.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/jquery-ui-1.10.4.custom.min").Include(
            //          "~/Content/NhanSu/jquery-ui-1.10.4.custom.min.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/jquery.news-ticker").Include(
            //          "~/Content/NhanSu/jquery.news-ticker.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/main").Include(
            //          "~/Content/NhanSu/main.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/nestable").Include(
            //          "~/Content/NhanSu/nestable.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/pace").Include(
            //          "~/Content/NhanSu/pace.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/style-responsive").Include(
            //          "~/Content/NhanSu/style-responsive.css"));

            //bundles.Add(new StyleBundle("~/Content/NhanSu/css/zabuto_calendar.min").Include(
            //          "~/Content/NhanSu/zabuto_calendar.min.css"));
        }
    }
}
