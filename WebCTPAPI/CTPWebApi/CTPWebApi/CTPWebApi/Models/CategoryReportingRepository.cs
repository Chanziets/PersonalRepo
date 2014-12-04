using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public class CategoryReportingRepository : ICategoryReportingRepository
    {
        private CtpWebContext db = new CtpWebContext();

        public IEnumerable<CategoryTopicReportDto> GetCategoryTopicReport()
        {
            var report = from c in db.Category
                         select new CategoryTopicReportDto()
                {
                    CategoryName = c.CategoryName,
                    LinkedTopicCount = (from t in db.Topic where t.CategoryId == c.CategoryId select t.TopicId).Count()
                };

            return report.AsEnumerable();
        }
    }
}