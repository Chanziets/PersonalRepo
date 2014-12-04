using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryUnitOfWorkPatterns.Models;

namespace RepositoryUnitOfWorkPatterns.Repositories
{
    public class TrainingTopicRepository : Repository<TrainingTopic>, ITrainingTopicRepository
    {
        public TrainingTopicRepository(TrainingDataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<TrainingTopic> GetTrainingTopicsPerCategory(Guid categoryId)
        {
            return dataContext.TrainingTopic.Where(t => t.CategoryId == categoryId).OrderBy(t => t.Order).ToList();
        }
    }
}