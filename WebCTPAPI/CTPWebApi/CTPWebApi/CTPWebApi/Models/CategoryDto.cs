using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CTPWebApi.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateAdded { get; set; }

        public List<TopicDto> Topics { get; set; }
    }
}