using System.Configuration;
using Autofac;
using Autofac.Integration.WebApi;
using Hermes.Logging;
using Hermes.Messaging.Bus;
using Hermes.Messaging.Configuration;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using RepositoryUnitOfWorkPatterns;
using RepositoryUnitOfWorkPatterns.Controllers;
using RepositoryUnitOfWorkPatterns.Models;
using RepositoryUnitOfWorkPatterns.Properties;
using RepositoryUnitOfWorkPatterns.Queries;
using RepositoryUnitOfWorkPatterns.Repositories;




[assembly: OwinStartup(typeof(Startup))]
namespace RepositoryUnitOfWorkPatterns
{
    public static class RouteNames
    {
        public static string DefaultApi = "defaultApi";
    }

    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
           InitializeLogging();

           LogFactory.BuildLogger = type => new Log4NetLogger(type);

            var config = new HttpConfiguration();
            ConfigureHttp(config);
            ConfigurePipeline(app, config);
            var container = RegisterServices();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //config.DependencyResolver = ((WebApiAutofacAdapter)Settings.RootContainer).BuildAutofacDependencyResolver();


        }

        private static void InitializeLogging()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var tracer = new TraceAppender();
            var patternLayout = new PatternLayout { ConversionPattern = "%d [%t] %-5p %c %m%n" };

            patternLayout.ActivateOptions();

            tracer.Layout = patternLayout;
            tracer.ActivateOptions();
            hierarchy.Root.AddAppender(tracer);

            var appender = new RollingFileAppender
            {
                Layout = patternLayout,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 4,
                MaximumFileSize = "1024KB",
                StaticLogFileName = true,
                File = ConfigurationManager.AppSettings["LogFileLocation"]
            };

            var emailAppender = new SmtpAppender
            {
                Layout = patternLayout,
                SmtpHost = "relay.clientele.local",
                From = "noreply@clientele.co.za",
                Subject = ConfigurationManager.AppSettings["Octopus.Environment.Name"] + "Application Forms Api Exception",
                To = "czietsman@clientele.co.za",
                Evaluator = new LevelEvaluator(Level.Fatal),
                BufferSize = 1,
                Lossy = true
            };

            appender.ActivateOptions();
            emailAppender.ActivateOptions();
            hierarchy.Root.AddAppender(appender);
            hierarchy.Root.AddAppender(emailAppender);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        //private static void InitializeEndPoint()
        //{
        //    var endpoint = new LocalEndpoint();

        //    endpoint.Start();
        //}

        private static void ConfigurePipeline(IAppBuilder app, HttpConfiguration config)
        {
            CorsConfig.Configure(app);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new TicketDataFormat(new ClearText())
            });

            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            app.UseWebApi(config);
        }

        private static void ConfigureHttp(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: RouteNames.DefaultApi,
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                });


            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        private IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new TrainingDataContext());

            builder.Register(c => new TrainingTopicRepository(c.Resolve<TrainingDataContext>()));
            builder.Register(c => new TopicQueryService(c.Resolve<TrainingTopicRepository>()));
            builder.Register(c => new TrainingTopicController(c.Resolve<TopicQueryService>()));

            builder.Register(c => new TrainingCategoryRepository(c.Resolve<TrainingDataContext>()));
            builder.Register(c => new CategoryQueryService(c.Resolve<TrainingCategoryRepository>()));
            builder.Register(c => new TrainingCategoryController(c.Resolve<CategoryQueryService>()));


            return builder.Build();
        }
    }
}