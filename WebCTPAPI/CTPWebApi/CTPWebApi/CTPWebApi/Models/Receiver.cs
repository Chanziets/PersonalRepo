using System;
using System.Collections.Generic;
using CTPWebApi.Models;

namespace CTPWebApi.Models
{
    public class Receiver
    {
        static ITrainingHistoryRepository trainingHistoryRepository = new TrainingHistoryRepository();

        public void LogTrainingHistory(TrainingHistory trainingHistoryRecord)
        {
            trainingHistoryRepository.Add(trainingHistoryRecord);
        }
    }
}