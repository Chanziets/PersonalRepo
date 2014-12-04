using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
     class LogTrainingHistoryCommand : Command
    {
        public LogTrainingHistoryCommand(Receiver receiver) : base(receiver)
        {
        }

        public override void LogTrainingHistory(TrainingHistory trainingHistoryRecord)
        {
            Receiver.LogTrainingHistory(trainingHistoryRecord);
        }
    }
}