using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles):base()
        {
            Roles = string.Join(",", roles);
        }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        public override Task OnAuthorizationAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal;
            if (!principal.Identity.IsAuthenticated)
            {
                return Task.FromResult(0);
            }

            if (Roles != string.Empty)
            {
                var roles = Roles.Split(',').ToList(); ;
                if (!IsInRole(roles, principal))
                {
                    var response = new HttpResponseMessage();
                    response.Content = new StringContent("{reason:\"用户未授权\"}");
                    actionContext.Response = response;
                    actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }

            return Task.FromResult(0);
        }

        private bool IsInRole(List<string> roles, IPrincipal principal)
        {
            var ret = true;
            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    ret = principal.IsInRole(role);
                    if (ret)
                    {
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
