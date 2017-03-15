using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.Cors;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Features.AttributeFilters;
using log4net;
using Infrastructure;
using Repository;
using IRepository;
using System.Reflection;
using System.Web.Http.ExceptionHandling;

namespace Webapi2.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:17315/");
            //开启跨域
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //路由
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
            builder.RegisterType<GlobalExceptionFilter>().WithAttributeFiltering().AsWebApiExceptionFilterFor<BaseController>().InstancePerRequest();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterApiControllers(Assembly.Load("Webapi")).WithAttributeFiltering();
            builder.RegisterWebApiFilterProvider(config);
            //config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionFilterForWebapi());
           // config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLoggerForWebapi());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync();
                Console.Write("http://localhost:17315/");
                Console.ReadLine();
            }
        }
    }
}
