using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
public class AuthorizationAttribute : AuthorizationFilterAttribute {
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public AuthorizationAttribute(string claimType, object claimValue) {
        this.ClaimType = claimType;
        this.ClaimValue = claimValue.ToString();
    }
    public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken) {
        var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
        if (!principal.Identity.IsAuthenticated) {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            return Task.FromResult(0);
        }
        if (!(principal.HasClaim(x => x.Type == ClaimType && x.Value == ClaimValue))) {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            return Task.FromResult(0);
        }
        //User is Authorized, complete execution
        return Task.FromResult(0);
    }
}