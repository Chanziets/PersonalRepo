using System;
using System.Collections.Generic;
using Clientele.Training.Persistence;

namespace Clientele.Training.WebApi.Repositories
{
    public interface ITrainingTopicRepository : IRepository<TrainingTopic>
    {
        IEnumerable<TrainingTopic> GetTrainingTopicsPerCategory(Guid categoryId);
    }
}