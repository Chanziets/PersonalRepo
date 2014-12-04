using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Clientele.Training.Persistence;
using Hermes.EntityFramework;

namespace Clientele.Training.WebApi.Queries
{
    public class TopicQueryService
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IDbSet<TrainingTopic> topicRepository;

        public TopicQueryService(IRepositoryFactory trainingTopicRepository)
        {
            repositoryFactory = trainingTopicRepository;
            topicRepository = repositoryFactory.GetRepository<TrainingTopic>();
        }


        public TrainingTopic Get(Guid trainingTopicId)
        {
            return topicRepository.Get(trainingTopicId);
        }

        public object GetTrainingTopicsByCategoryId(Guid categoryId)
        {
            var topics = topicRepository.Select(e => new
          {
              e.TopicId,
              e.CategoryId,
              e.TopicName
          }).Where(t => t.CategoryId == categoryId);

            return topics;
        }

    }
}