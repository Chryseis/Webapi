using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;

namespace Infrastructure
{
    public class GlobalAuthorizationFilter:IAutofacAuthorizationFilter
    {

        public Task OnAuthorizationAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
