using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;
using System.Web.Http.Controllers;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Infrastructure
{
    public class GlobalAuthorizationFilter:IAutofacAuthorizationFilter
    {

        public Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var auth = new AuthorizeAttribute(); 
            //var response = new HttpResponseMessage();
            //response.Content = new StringContent("{'a':1}");
            //actionContext.Response = response;
            //actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            auth.Roles = "admin";
            return Task.FromResult(0);
        }
    }
}
