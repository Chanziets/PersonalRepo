using System.Data.Entity;
using System.Web.Http;

namespace DynamicWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            Database.SetInitializer(new DynamicContextInitializer());
            var context = new DynamicDbContext();
            context.Database.Initialize(true);
        }
    }
}
