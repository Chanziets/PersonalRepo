using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Clientele.Training.Contracts;
using Clientele.Training.Persistence;
using Hermes.EntityFramework;
using Hermes.Messaging;

namespace Clientele.Training.ApplicationService
{
    public class TrainingCategoryCommandService : IHandleMessage<CreateTrainingCategory>, IHandleMessage<UpdateTrainingCategory>, IHandleMessage<DeleteTrainingCategory>
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IDbSet<TrainingCategory> categoryRepository;

        public TrainingCategoryCommandService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            categoryRepository = repositoryFactory.GetRepository<TrainingCategory>();
        }

        public void Handle(CreateTrainingCategory m)
        {
            categoryRepository.Add(new TrainingCategory()
            {
                CategoryId = m.Id, 
                CategoryName = m.Name, 
                DateAdded = DateTime.Now
            });
            
        }

        public void Handle(UpdateTrainingCategory m)
        {
            categoryRepository.AddOrUpdate(new TrainingCategory()
            {
                CategoryId = m.Id,
                CategoryName = m.Name
            });
        }

        public void Handle(DeleteTrainingCategory m)
        {
            categoryRepository.Remove(new TrainingCategory() {CategoryId = m.Id});
        }
    }
}
