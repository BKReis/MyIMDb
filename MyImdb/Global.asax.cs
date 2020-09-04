using BusinessLogic.Data;
using Lacuna.CommonEntityFramework;
using MyImdb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyImdb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool InMaintenanceMode { get; set; }
        public static DatabaseStates DatabaseState { get; set; }
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            logger.Trace("Application starting");
            try {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                GlobalFilters.Filters.Add(new MaintenanceFilterAttribute());
                CheckDatabase();
                logger.Trace("Application started");
            }
            catch (Exception ex) {
                logger.Fatal(ex, "Application startup error");
                throw;
            }
        }
        public static void CheckDatabase() {
            DatabaseState = ApplicationDbContext.CheckDatabase();
            InMaintenanceMode = DatabaseState != DatabaseStates.OK;
        }
    }
}
