using System;
using System.ComponentModel.DataAnnotations;

namespace RepositoryUnitOfWorkPatterns.Models
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