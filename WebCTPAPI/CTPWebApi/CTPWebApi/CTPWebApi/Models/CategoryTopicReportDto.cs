using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public class CategoryTopicReportDto
    {
        public string CategoryName { get; set; }
        public int LinkedTopicCount { get; set; }
    }
}