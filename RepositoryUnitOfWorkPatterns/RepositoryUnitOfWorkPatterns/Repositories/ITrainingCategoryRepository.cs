using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryUnitOfWorkPatterns.Models;

namespace RepositoryUnitOfWorkPatterns.Repositories
{
    public interface ITrainingCategoryRepository : IRepository<TrainingCategory>
    {
        IEnumerable<TrainingCategoryTopicReport> GetCategoryTopicReport();
    }
}