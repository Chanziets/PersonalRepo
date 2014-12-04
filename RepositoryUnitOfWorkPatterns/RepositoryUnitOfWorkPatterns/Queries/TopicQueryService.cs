using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryUnitOfWorkPatterns.Models;
using RepositoryUnitOfWorkPatterns.Repositories;

namespace RepositoryUnitOfWorkPatterns.Queries
{
    public class TopicQueryService 
    {
        private readonly ITrainingTopicRepository trainingTopicRepository;

        public TopicQueryService(ITrainingTopicRepository trainingTopicRepository)
        {
            this.trainingTopicRepository = trainingTopicRepository;
        }

        public List<TrainingTopic> GetAllTrainingTopics()
        {
            return trainingTopicRepository.GetAll().ToList();
        }

        public TrainingTopic Get(Guid trainingTopicId)
        {
            return trainingTopicRepository.GetById(tc => tc.TopicId == trainingTopicId);
        }

        public IEnumerable<TrainingTopic> GetTrainingTopicsByCategoryId(Guid categoryId)
        {
            return trainingTopicRepository.GetTrainingTopicsPerCategory(categoryId);
        }

        public void Run(System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}