using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyImdb;

public class MaintenanceModeFilterAttribute : ActionFilterAttribute {
    public override void OnActionExecuting(HttpActionContext actionContext) {
        if (MvcApplication.InMaintenanceMode) {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "The server is currently undergoing maintenance. Please try again later.");
        }
    }
}