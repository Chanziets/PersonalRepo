using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models.InputDtos
{
    public class CategoryTopicSearch
    {
        public Guid CategoryId { get; set; }
        public string SearchTopic { get; set; }
    }
}