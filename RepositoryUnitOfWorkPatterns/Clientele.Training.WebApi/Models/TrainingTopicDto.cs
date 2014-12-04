using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clientele.Training.WebApi.Models
{
    public class TrainingTopicDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }

    }
}