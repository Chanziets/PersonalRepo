using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public interface ICategoryReportingRepository
    {
        IEnumerable<CategoryTopicReportDto> GetCategoryTopicReport();
    }
}