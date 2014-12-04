using System;
using System.ComponentModel.DataAnnotations;

namespace CTPWebApi.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required] 
        public string CategoryName { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }


    }
}