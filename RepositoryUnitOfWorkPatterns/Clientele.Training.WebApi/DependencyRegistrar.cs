using Clientele.Training.WebApi.Queries;
using Hermes.Ioc;

namespace Clientele.Training.WebApi
{
    public class DependencyRegistrar : IRegisterDependencies
    {
        public void Register(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CategoryQueryService>();
            containerBuilder.RegisterType<TopicQueryService>();

        }
    }
}