using System.Web.Http;
using System.Data.Entity;
using RepositoryUnitOfWorkPatterns.Models;

namespace RepositoryUnitOfWorkPatterns
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            Database.SetInitializer(new TrainingDataContextIntializer());
            var context = new TrainingDataContext();
            context.Database.Initialize(true);
        }
    }
}