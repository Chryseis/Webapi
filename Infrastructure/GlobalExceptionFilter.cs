using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Autofac.Integration.WebApi;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http.Filters;
using log4net;
using Autofac;
using Autofac.Features.AttributeFilters;

namespace Infrastructure
{
    public class GlobalExceptionFilter:IAutofacExceptionFilter
    {
        private ILog _logger;

        public GlobalExceptionFilter([KeyFilter("ExceptionLog")] ILog logger)
        {
            this._logger=logger;
        }

        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext,CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            var responseContent = new ResponseBase();
            responseContent.SetReponse(StatusCode.InternalServerError, actionExecutedContext.Exception.Message);
            _logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(responseContent));
            actionExecutedContext.Response = response;
            actionExecutedContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Task.FromResult(response);
        }
    }
}
