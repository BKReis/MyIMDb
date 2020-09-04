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
            bundles.Add(new ScriptBundle("~/bundles/accountLogin").Include("~/Scripts/app/accountLogin.js"));
            bundles.Add(new ScriptBundle("~/bundles/accountRegister").Include("~/Scripts/app/accountRegister.js"));
            bundles.Add(new ScriptBundle("~/bundles/commentCreate").Include("~/Scripts/app/commentCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/commentEdit").Include("~/Scripts/app/commentEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/commentDelete").Include("~/Scripts/app/commentDelete.js"));
            bundles.Add(new ScriptBundle("~/bundles/movies").Include("~/Scripts/app/movies.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieCreate").Include("~/Scripts/app/movieCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieEdit").Include("~/Scripts/app/movieEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieDelete").Include("~/Scripts/app/movieDelete.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieDetails").Include("~/Scripts/app/movieDetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/genres").Include("~/Scripts/app/genres.js"));
            bundles.Add(new ScriptBundle("~/bundles/genreCreate").Include("~/Scripts/app/genreCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/genreEdit").Include("~/Scripts/app/genreEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/genreDelete").Include("~/Scripts/app/genreDelete.js"));
            bundles.Add(new ScriptBundle("~/bundles/actors").Include("~/Scripts/app/actors.js"));
            bundles.Add(new ScriptBundle("~/bundles/actorCreate").Include("~/Scripts/app/actorCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/actorEdit").Include("~/Scripts/app/actorEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/actorDelete").Include("~/Scripts/app/actorDelete.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieActorCreate").Include("~/Scripts/app/movieActorCreate.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieActorEdit").Include("~/Scripts/app/movieActorEdit.js"));
            bundles.Add(new ScriptBundle("~/bundles/movieActorDelete").Include("~/Scripts/app/movieActorDelete.js"));
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
