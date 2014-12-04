using System;
using System.Runtime.Serialization;

namespace Clientele.Training.Contracts
{
    [DataContract]
    public class DeleteTrainingTopic : ITrainingCommand
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        protected DeleteTrainingTopic()
        {
            
        }

        public DeleteTrainingTopic(Guid id)
        {
            Id = id;
        }
    }
}
