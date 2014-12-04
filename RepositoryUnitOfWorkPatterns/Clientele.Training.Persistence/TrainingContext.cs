using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Hermes;

namespace Clientele.Training.Persistence
{
    public class TrainingContext: DbContext
    {
        private static readonly string MutexKey;

      
        public IDbSet<TrainingCategory> TrainingCategory { get; set; }
        public IDbSet<TrainingTopic> TrainingTopic { get; set; }


        static TrainingContext()
        {
            MutexKey = DeterministicGuid.Create(typeof(TrainingContext).FullName).ToString();
        }

        public TrainingContext()
        {
        }

        public TrainingContext(string databaseName)
            : base(databaseName)
        {
        }

        internal void InitializeLookupTables()
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            ConfigurationInitialiser.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }




}
