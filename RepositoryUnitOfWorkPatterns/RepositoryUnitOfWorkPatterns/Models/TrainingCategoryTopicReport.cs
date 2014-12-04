using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryUnitOfWorkPatterns.Models
{
    public class TrainingCategoryTopicReport
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int LinkedTopicCount { get; set; }
    }
}