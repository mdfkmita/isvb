﻿using System.Web;
using System.Web.Optimization;

namespace isvb.dev
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/dashboard.js").Include(
                     "~/Scripts/dashboard.js"));
            bundles.Add(new ScriptBundle("~/bundles/cartJs").Include(
                    "~/Scripts/cartJS.js"));
            bundles.Add(new ScriptBundle("~/bundles/mycart.js").Include(
                    "~/Scripts/mycart.js"));
            bundles.Add(new ScriptBundle("~/bundles/countrows.js").Include(
                    "~/Scripts/countrows.js"));
            bundles.Add(new ScriptBundle("~/bundles/doajax").Include(
                    "~/Scripts/doAjax.js"));
        }
    }
}
