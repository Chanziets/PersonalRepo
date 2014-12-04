using System;
using System.ComponentModel.DataAnnotations;

namespace Clientele.Training.Persistence
{
    public class TrainingCategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
    }
}