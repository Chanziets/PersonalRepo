using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Web;
using RepositoryUnitOfWorkPatterns.Models;

namespace RepositoryUnitOfWorkPatterns.Repositories
{
    public class TrainingCategoryRepository : Repository<TrainingCategory>, ITrainingCategoryRepository
    {
        public TrainingCategoryRepository(TrainingDataContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<TrainingCategoryTopicReport> GetCategoryTopicReport()
        {

            var report = from c in dataContext.TrainingCategory
                         select new TrainingCategoryTopicReport()
                         {
                             CategoryId = c.CategoryId,
                             CategoryName = c.CategoryName,
                             LinkedTopicCount = dataContext.TrainingTopic.Count(t => t.CategoryId == c.CategoryId)
                         };

           
            return report.AsEnumerable();
        }

    
    }
}