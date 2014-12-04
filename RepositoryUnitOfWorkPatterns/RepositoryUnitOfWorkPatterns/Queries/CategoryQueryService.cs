using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryUnitOfWorkPatterns.Models;
using RepositoryUnitOfWorkPatterns.Repositories;

namespace RepositoryUnitOfWorkPatterns.Queries
{
    public class CategoryQueryService
    {
        private readonly ITrainingCategoryRepository trainingCategoryRepository;

        public CategoryQueryService(ITrainingCategoryRepository trainingCategoryRepository)
        {
            this.trainingCategoryRepository = trainingCategoryRepository;
        }

        public List<TrainingCategory> GetAllTrainingCategories()
        {
            return trainingCategoryRepository.GetAll().ToList();
        }

        public TrainingCategory Get(Guid trainingCategoryId)
        {
            return trainingCategoryRepository.GetById(tc => tc.CategoryId == trainingCategoryId);
        }

        public IEnumerable<TrainingCategoryTopicReport> GetCategoryTopicReport()
        {
            return trainingCategoryRepository.GetCategoryTopicReport();
        }


    }
}