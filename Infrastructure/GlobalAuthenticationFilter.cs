using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;

namespace Infrastructure
{
    public class GlobalAuthenticationFilter:IAutofacAuthenticationFilter
    {

        public Task AuthenticateAsync(System.Web.Http.Filters.HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(System.Web.Http.Filters.HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
