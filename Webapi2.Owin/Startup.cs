using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Features.AttributeFilters;
using log4net;
using Infrastructure;
using System.Reflection;
using Repository;
using IRepository;

[assembly: OwinStartup(typeof(Webapi2.Owin.Startup))]

namespace Webapi2.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            Webapi.AutoMapper.Configuration.Configure();

            var config = new HttpConfiguration();

            //属性路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.EnsureInitialized();

            //注册autofac
            var builder = new ContainerBuilder();
            builder.Register(c => LogManager.GetLogger("GlobalLog")).Keyed("GlobalLog", typeof(ILog));
            builder.Register(c => LogManager.GetLogger("ExceptionLog")).Keyed("ExceptionLog", typeof(ILog));
            builder.RegisterType<GlobalAuthenticationFilter>().AsWebApiAuthenticationFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalAuthorizationFilter>().AsWebApiAuthorizationFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalActionFilter>().AsWebApiActionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<GlobalExceptionFilter>().WithAttributeFiltering().AsWebApiExceptionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterApiControllers(Assembly.Load("Webapi")).WithAttributeFiltering();
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            app.UseAutofacWebApi(config);

            app.UseWebApi(config);
        }
    }
}
