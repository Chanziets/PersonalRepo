using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public class TrainingHistoryRepository : ITrainingHistoryRepository
    {
        private CtpWebContext db = new CtpWebContext();

        public IEnumerable<TrainingHistory> GetAllTrainingHistory()
        {
            return db.TrainingHistory;
        }

        public void Add(TrainingHistory trainingHistory)
        {
            if (trainingHistory == null)
            {
                throw new ArgumentNullException("trainingHistory");
            }

            trainingHistory.TrainingHistoryId = Guid.NewGuid();
            trainingHistory.DateAdded = DateTime.Now;
            trainingHistory.Username = "Channel";

            db.TrainingHistory.Add(trainingHistory);
            db.SaveChanges();

        }

    }
}