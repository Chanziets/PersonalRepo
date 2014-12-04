using System;
using System.Runtime.Serialization;

namespace Clientele.Training.Contracts
{
    [DataContract]
    public class CreateTrainingCategory : ITrainingCommand
    {
        [DataMember (Name = "Id")]
        public Guid Id { get; private set; }

        [DataMember(Name = "Name")]
        public string Name { get; private set; }

        protected CreateTrainingCategory()
        {
            
        }

        public CreateTrainingCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
