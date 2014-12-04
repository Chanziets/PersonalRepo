using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Routing;
using System.Data.Entity;
using CTPWebApi.Models;

namespace CTPWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            Database.SetInitializer(new CtpWebContextInitializer());
            var context = new CtpWebContext();
            context.Database.Initialize(true);
        }
    }
}
