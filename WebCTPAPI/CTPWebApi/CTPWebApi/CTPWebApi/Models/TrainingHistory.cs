using System;
using System.ComponentModel.DataAnnotations;

namespace CTPWebApi.Models
{
    public class TrainingHistory
    {

        [Key]
        public Guid TrainingHistoryId { get; set; }

        public string Username { get; set; }

        public string EntityDetails { get; set; }

        public string Action { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

    }
}