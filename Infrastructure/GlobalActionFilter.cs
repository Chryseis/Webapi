using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;
using System.Web.Http.Filters;
using System.Threading;
using System.Web.Http.Controllers;

namespace Infrastructure
{
    public class GlobalActionFilter:IAutofacActionFilter
    {

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {            
            return Task.FromResult(new object());
        }
    }
}
