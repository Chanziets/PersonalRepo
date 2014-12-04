using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryUnitOfWorkPatterns.Models;

namespace RepositoryUnitOfWorkPatterns.Repositories
{
    public interface ITrainingTopicRepository : IRepository<TrainingTopic>
    {
        IEnumerable<TrainingTopic> GetTrainingTopicsPerCategory(Guid categoryId);
    }
}