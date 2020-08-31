using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyImdb.App_Start {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            //app
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/app.js", "~/Scripts/app/lib.js"));
            bundles.Add(new ScriptBundle("~/bundles/movies").Include("~/Scripts/app/movies.js"));
            bundles.Add(new ScriptBundle("~/bundles/genres").Include("~/Scripts/app/genres.js"));
            bundles.Add(new ScriptBundle("~/bundles/genreCreate").Include("~/Scripts/app/genreCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/dialog").Include("~/Scripts/app/Dialog/favoriteGenre.js"));
            //Script Bundles
            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js", "~/Scripts/angular-route.js","~/Scripts/angular-block-ui.js","~/Scripts/pnotify.js","~/Scripts/app/directives.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib").Include("~/Scripts/jquery-{version}.js",
            "~/Scripts/bootstrap.js", "~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/reset-form.js", "~/Scripts/fontawesome/all.js"));
            bundles.Add(new ScriptBundle("~/bundles/uiBootstrap").Include("~/Scripts/angular-ui/ui-bootstrap.js", "~/Scripts/angular-ui/ui-bootstrap-tpls.js","~/Scripts/ng-table.js"));
            //Style Bundles
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/Site.css","~/Content/home-images.css","~/Content/angular-block-ui.css","~/Content/pnotify.css","~/Content/ng-table.css"));
        }
    }
}
