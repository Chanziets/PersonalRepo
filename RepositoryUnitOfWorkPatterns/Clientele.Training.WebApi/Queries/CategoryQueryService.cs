using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Clientele.Training.Persistence;
using Hermes.EntityFramework;


namespace Clientele.Training.WebApi.Queries
{
    public class CategoryQueryService
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IDbSet<TrainingCategory> categoryRepository;

        public CategoryQueryService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            categoryRepository = repositoryFactory.GetRepository<TrainingCategory>();
        }

        public List<TrainingCategory> GetAllTrainingCategories()
        {
            return categoryRepository.ToList();
        }

        public TrainingCategory Get(Guid trainingCategoryId)
        {
            return categoryRepository.Get(trainingCategoryId);
        }

      
    }
}