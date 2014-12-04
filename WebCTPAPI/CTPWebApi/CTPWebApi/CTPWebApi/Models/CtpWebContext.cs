using System.Data.Entity;

namespace CTPWebApi.Models
{
    public class CtpWebContext : DbContext
    {
        public CtpWebContext() : base("name=CtpWebContext") { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<TopicAttachment> TopicAttachment { get; set; }
        public DbSet<TrainingHistory> TrainingHistory { get; set; }
    }
}