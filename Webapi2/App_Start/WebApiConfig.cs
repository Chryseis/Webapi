using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Features.AttributeFilters;
using Infrastructure;
using IRepository;
using Repository;
using System.Reflection;
using System.Web.Http.Cors;
using log4net;
using System.Web.Http.ExceptionHandling;

namespace Webapi2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //开启跨域
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);

            //属性路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           

            //注册autofac
            var builder = new ContainerBuilder();
            builder.Register(c => LogManager.GetLogger("GlobalLog")).Keyed("GlobalLog", typeof(ILog));
            builder.Register(c => LogManager.GetLogger("ExceptionLog")).Keyed("ExceptionLog", typeof(ILog));
            builder.RegisterType<GlobalAuthenticationFilter>().AsWebApiAuthenticationFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalAuthorizationFilter>().AsWebApiAuthorizationFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalActionFilter>().AsWebApiActionFilterFor<BaseController>().InstancePerRequest();
           // builder.RegisterType<GlobalExceptionFilter>().WithAttributeFiltering().AsWebApiExceptionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterApiControllers(Assembly.Load("Webapi")).WithAttributeFiltering();
            builder.RegisterWebApiFilterProvider(config);
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionFilterForWebapi());
            config.Services.Replace(typeof(IExceptionLogger),new GlobalExceptionLoggerForWebapi());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}