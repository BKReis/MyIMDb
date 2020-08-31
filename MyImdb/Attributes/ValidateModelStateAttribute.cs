using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace MyImdb.Attributes {
    public class ValidateModelStateAttribute : ActionFilterAttribute {
        private static string FirstLetterLowerCase(string input) {
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
        private static string getError(ModelErrorCollection errors) {
            var errorWithMessage = errors.FirstOrDefault(e => !string.IsNullOrEmpty(e.ErrorMessage));
            return (errorWithMessage != null ? errorWithMessage.ErrorMessage : "UnspecifiedError");
        }
        public override void OnActionExecuting(HttpActionContext actionContext) {
            if (!actionContext.ModelState.IsValid) {
                var validationErrors = actionContext.ModelState.Where(m => m.Value.Errors.Any()).
                Select(m => new {
                    Field = FirstLetterLowerCase(m.Key.Substring(m.Key.IndexOf('.', 0) + 1).Replace("[", "_").Replace("]", "_")),
                    Error = getError(m.Value.Errors)
                }).ToList();
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new {
                    Message = string.Format("Validation error(s): {0}", string.Join("\r\n", validationErrors.Select(e => string.Format("- {0}: {1}", e.Field, e.Error)))),
                    ValidationErrors = validationErrors
                });
            }
        }
    }
}