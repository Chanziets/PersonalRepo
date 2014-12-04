using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    abstract class Command
    {
        protected Receiver Receiver;

        public Command(Receiver receiver)
        {
            Receiver = receiver;
        }

        public abstract void LogTrainingHistory(TrainingHistory trainingHistoryRecord);
    }
}