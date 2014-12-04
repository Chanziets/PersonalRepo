using System;
using System.Runtime.Serialization;

namespace Clientele.Training.Contracts
{
    [DataContract]
    public class UpdateTrainingCategory : ITrainingCommand
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; private set; }

        [DataMember(Name = "Name")]
        public string Name { get; private set; }

        protected UpdateTrainingCategory()
        {

        }

        public UpdateTrainingCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
