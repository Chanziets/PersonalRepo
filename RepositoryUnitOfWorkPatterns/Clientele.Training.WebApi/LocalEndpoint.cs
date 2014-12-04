using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Clientele.Training.Contracts;
using Clientele.Training.Persistence;
using Hermes.EntityFramework;
using Hermes.Messaging;
using Hermes.Messaging.EndPoints;
using Hermes.Messaging.Transports.SqlTransport;
using Hermes.Serialization.Json;

namespace Clientele.Training.WebApi
{
    public class LocalEndpoint : LocalEndpoint<WebApiAutofacAdapter>
    {
        protected override void ConfigureEndpoint(IConfigureEndpoint configuration)
        {
            const string connectionString = "TrainingContext";

            configuration
                .UseJsonSerialization()
                .UseSqlTransport("SqlTransport")
                .DefineEventAs(IsEvent)
                .DefineCommandAs(IsCommand)
                .RegisterDependencies<DependencyRegistrar>()
                .ConfigureEntityFramework<TrainingContext>(connectionString)
                .UserNameResolver(UserIdResolver);
        }

        private static string UserIdResolver()
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null || String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                return Environment.UserName;

            return HttpContext.Current.User.Identity.Name;
        }

        public Guid GetUserIdFromIdentity()
        {
            var userId = Guid.Empty;

            if (HttpContext.Current.User == null)
            {
                return userId;
            }

            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;
                var userIdClaim = claims.FirstOrDefault(claim => claim.Type.Equals("urn:user:user_id", StringComparison.InvariantCultureIgnoreCase));
                if (userIdClaim != null)
                {
                    Guid.TryParse(userIdClaim.Value, out userId);
                }
            }

            return userId;
        }

        private static bool IsCommand(Type type)
        {
            if (type == null || type.Namespace == null)
                return false;

            var isCommand = typeof(ITrainingCommand).IsAssignableFrom(type);

            return isCommand;
        }

        private static bool IsEvent(Type type)
        {
            if (type == null || type.Namespace == null)
                return false;

            return false;
        }
    }
}