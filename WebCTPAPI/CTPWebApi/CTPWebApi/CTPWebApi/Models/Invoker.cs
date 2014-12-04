using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    class Invoker
    {
        private Command command;

        public void SetCommand(Command command)
        {
            this.command = command;
        }

        public void ExecuteCommand(TrainingHistory trainingHistoryRecord)
        {
            command.LogTrainingHistory(trainingHistoryRecord);
        }
    }
}