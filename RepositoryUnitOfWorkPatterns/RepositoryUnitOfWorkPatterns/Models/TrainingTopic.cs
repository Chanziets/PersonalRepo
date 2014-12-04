using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryUnitOfWorkPatterns.Models
{
    public class TrainingTopic 
    {
        [Key]
        public Guid TopicId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual TrainingCategory Category { get; set; }
        public Guid CategoryId { get; set; }

        [Required]
        public string TopicName { get; set; }
        [Required]
        public string TopicContext { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
    }
}