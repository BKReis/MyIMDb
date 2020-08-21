using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyImdb.App_Start {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            //Script Bundles
            bundles.Add(new ScriptBundle("~/bundles/lib").Include("~/Scripts/jquery-{version}.js",
            "~/Scripts/bootstrap.js", "~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/reset-form.js", "~/Scripts/fontawesome/all.js"));
            //Style Bundles
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/Site.css","~/Content/home-images.css"));
        }
    }
}
