﻿using System.Web.Optimization;

namespace WorldCup
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.lumen.min.css", "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/bootstrap-chosen.css",
                "~/Content/site.css", "~/Content/Gridmvc.css", "~/Content/toastr.css",
                "~/Content/flagsFlat.png.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/moment-with-langs.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/chosen.jquery.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include("~/Scripts/gridmvc.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include("~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/globalize/globalize.js", "~/Scripts/dx.chartjs.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include("~/Scripts/bootstrap-slider.js"));
        }
    }
}
