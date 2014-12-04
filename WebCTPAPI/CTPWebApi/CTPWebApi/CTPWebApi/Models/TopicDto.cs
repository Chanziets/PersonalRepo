using System;

namespace CTPWebApi.Models
{
    public class TopicDto
    {
        public Guid TopicId { get; set; }
        public Guid CategoryId { get; set; }
        public string TopicName { get; set; }
        public string TopicContext { get; set; }
        public int PriorityWeight { get; set; }
        public DateTime DateAdded { get; set; }

    }
}