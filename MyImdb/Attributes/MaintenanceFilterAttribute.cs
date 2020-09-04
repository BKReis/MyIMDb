using MyImdb;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

public class MaintenanceFilterAttribute : System.Web.Mvc.ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext filterContext) {
        var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
        var maintenanceController = nameof(MaintenanceController).Replace("Controller", string.Empty);
        if (MvcApplication.InMaintenanceMode) {
            if (controller != maintenanceController) {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {{ "Controller", maintenanceController },
{ "Action", "Index" } });
            }
        }
    }
}