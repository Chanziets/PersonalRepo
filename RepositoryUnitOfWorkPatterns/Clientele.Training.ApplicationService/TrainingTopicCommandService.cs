using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Clientele.Training.Contracts;
using Clientele.Training.Persistence;
using Hermes.EntityFramework;
using Hermes.Messaging;

namespace Clientele.Training.ApplicationService
{
    public class TrainingTopicCommandService : IHandleMessage<CreateTrainingTopic>, IHandleMessage<UpdateTrainingTopic>,IHandleMessage<DeleteTrainingTopic>
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IDbSet<TrainingTopic> topicRepository;

        public TrainingTopicCommandService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            topicRepository = repositoryFactory.GetRepository<TrainingTopic>();
        }

        public void Handle(CreateTrainingTopic m)
        {
            topicRepository.Add(new TrainingTopic()
            {
                TopicId = m.Id,
                CategoryId = m.CategoryId,
                TopicName = m.Name,
                TopicContext = m.Context,
                DateAdded = DateTime.Now

            });
        }

        public void Handle(UpdateTrainingTopic m)
        {
            topicRepository.AddOrUpdate(new TrainingTopic()
            {
                TopicId = m.Id,
                TopicName = m.Name,
                TopicContext = m.Context,
                CategoryId = m.CategoryId
            });
        }

        public void Handle(DeleteTrainingTopic m)
        {
            topicRepository.Remove(new TrainingTopic() {TopicId = m.Id});
        }
    }
}
