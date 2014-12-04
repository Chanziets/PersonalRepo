using System;
using System.Runtime.Serialization;

namespace Clientele.Training.Contracts
{
    [DataContract]
    public class DeleteTrainingCategory : ITrainingCommand
    {
        [DataMember]
        public Guid Id { get; private set; }

        protected DeleteTrainingCategory()
        {
            
        }

        public DeleteTrainingCategory(Guid id)
        {
            Id = id;
        }

        
    }
}