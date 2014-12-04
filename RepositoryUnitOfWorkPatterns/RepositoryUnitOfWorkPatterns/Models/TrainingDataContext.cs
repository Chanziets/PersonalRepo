using System.Data.Entity;
using System.Data.Linq;

namespace RepositoryUnitOfWorkPatterns.Models
{
    public class TrainingDataContext : DbContext
    {
        public TrainingDataContext() : base("name=TrainingDataContext") { }

        public DbSet<TrainingCategory> TrainingCategory { get; set; }
        public DbSet<TrainingTopic> TrainingTopic { get; set; }
    }
}