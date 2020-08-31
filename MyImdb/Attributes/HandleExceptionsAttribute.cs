using BusinessLogic.Exceptions;
using NLog;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

public class HandleExceptionsAttribute : ExceptionFilterAttribute {
    private static Logger logger = LogManager.GetCurrentClassLogger();
    public override void OnException(HttpActionExecutedContext actionExecutedContext) {
        var exception = actionExecutedContext.Exception;
        if (exception is ErrorModelException) {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse((HttpStatusCode)422, ((ErrorModelException)exception).ErrorModel);
        }
        else {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An unexpected error has ocurred.");
            logger.Error(exception, "Unexpected error");
        }
        base.OnException(actionExecutedContext);
    }
}