using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepositoryUnitOfWorkPatterns.Repositories;


namespace RepositoryUnitOfWorkPatterns.Models
{
    public class TrainingUnitOfWork : IDisposable
    {

        private readonly TrainingDataContext dataContext = new TrainingDataContext();
        private Repository<TrainingCategory> trainingCategoryRepository;
        private Repository<TrainingTopic> trainingTopicRepository;

        public Repository<TrainingCategory> TrainingCategoryRepository
        {
            get {
                return trainingCategoryRepository ??
                       (trainingCategoryRepository = new TrainingCategoryRepository(dataContext));
            }
        }

        public Repository<TrainingTopic> TrainingTopicRepository
        {
            get
            {
                return trainingTopicRepository ??
                       (trainingTopicRepository = new TrainingTopicRepository(dataContext));
            }
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                dataContext.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}