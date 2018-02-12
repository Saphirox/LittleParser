

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using LittleParser.Web.IoC;
using Ninject;

namespace LittleParser.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(new StandardKernel());
        }
    }
}