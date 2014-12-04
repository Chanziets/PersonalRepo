using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public interface ITrainingHistoryRepository
    {
        IEnumerable<TrainingHistory> GetAllTrainingHistory();

        void Add(TrainingHistory trainingActionHistory);

    }
}