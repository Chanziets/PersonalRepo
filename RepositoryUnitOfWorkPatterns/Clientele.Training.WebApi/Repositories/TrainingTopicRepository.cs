using System;
using System.Collections.Generic;
using System.Linq;
using Clientele.Training.Persistence;

namespace Clientele.Training.WebApi.Repositories
{
    public class TrainingTopicRepository : Repository<TrainingTopic>, ITrainingTopicRepository
    {
        public TrainingTopicRepository(TrainingContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<TrainingTopic> GetTrainingTopicsPerCategory(Guid categoryId)
        {
            return dataContext.TrainingTopic.Where(t => t.CategoryId == categoryId).OrderBy(t => t.Order).ToList();
        }
    }
}