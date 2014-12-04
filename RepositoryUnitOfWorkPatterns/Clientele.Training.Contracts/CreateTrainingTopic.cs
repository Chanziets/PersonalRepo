using System;
using System.Runtime.Serialization;

namespace Clientele.Training.Contracts
{
    [DataContract]
    public class CreateTrainingTopic : ITrainingCommand
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; private set; }
        [DataMember(Name = "Name")]
        public string Name { get; private set; }
        [DataMember(Name = "Context")]
        public string Context { get; private set; }
        [DataMember(Name = "CategoryId")]
        public Guid CategoryId { get; private set; }

        protected CreateTrainingTopic()
        {

        }

        public CreateTrainingTopic(Guid id, string name, string context, Guid categoryId)
        {
            Id = id;
            Name = name;
            Context = context;
            CategoryId = categoryId;
        }
    }
}
