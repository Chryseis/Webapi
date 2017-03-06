using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using IRepository;
using Repository;
using System.Reflection;
using System.Web.Http.Cors;

namespace Webapi2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //开启跨域
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<GlobalAuthorizationFilter>().AsWebApiAuthorizationFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalActionFilter>().AsWebApiActionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalExceptionFilter>().AsWebApiExceptionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.Load("Webapi"));
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}