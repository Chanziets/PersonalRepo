using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CTPWebApi.Models;

namespace CTPWebApi.Controllers
{
    public class CategoryTopicReportController : ApiController
    {
        static readonly ICategoryReportingRepository _CategoryReporting = new CategoryReportingRepository();

        [HttpGet]
        public HttpResponseMessage GetCategoryTopicReport()
        {
            try
            {
                var categoryTopicReport = _CategoryReporting.GetCategoryTopicReport();

                if (categoryTopicReport == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, categoryTopicReport);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}