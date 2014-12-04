using Clientele.Training.Persistence;

namespace Clientele.Training.WebApi.Repositories
{
    public class TrainingCategoryRepository : Repository<TrainingCategory>, ITrainingCategoryRepository
    {
        public TrainingCategoryRepository(TrainingContext dataContext)
            : base(dataContext)
        {
        }
    
    }
}