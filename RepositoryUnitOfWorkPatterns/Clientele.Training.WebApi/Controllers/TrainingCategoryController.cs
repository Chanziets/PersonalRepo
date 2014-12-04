using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using Clientele.Training.Contracts;
using Clientele.Training.WebApi.Models;
using Clientele.Training.WebApi.Queries;
using Hermes.Messaging.Bus;


namespace Clientele.Training.WebApi.Controllers
{
    public class TrainingCategoryController : ApiController
    {
        private readonly LocalBus localBus;
        private readonly CategoryQueryService categoryQueryService;

        public TrainingCategoryController(LocalBus localBus, CategoryQueryService categoryQueryService)
        {
            this.localBus = localBus;
            this.categoryQueryService = categoryQueryService;
        }

        public IEnumerable<object> GetAllTrainingCategories()
        {
            return categoryQueryService.GetAllTrainingCategories().ToList();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid categoryId)
        {
            try
            {
                var traingingCategory = categoryQueryService.Get(categoryId);

                if (traingingCategory == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, traingingCategory);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostTrainingCategory(TrainingCategoryDto trainingCategory)
        {
            try
            {
                var id = SequentialGuid.New();
                var createTrainingCategory = new CreateTrainingCategory(id, trainingCategory.Name);
                localBus.Execute(createTrainingCategory);
                var response = Request.CreateResponse(HttpStatusCode.Created, trainingCategory);

                string uri = Url.Link("DefaultApi", new { id = createTrainingCategory.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutTrainingCategory(TrainingCategoryDto trainingCategory)
        {
            try
            {
                var updateTrainingCategory = new UpdateTrainingCategory(trainingCategory.Id,trainingCategory.Name);
                localBus.Execute(updateTrainingCategory);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteTrainingCategory(Guid categoryId)
        {
            try
            {
                var deleteTrainingCategory = new DeleteTrainingCategory(categoryId);
                localBus.Execute(deleteTrainingCategory);
                
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}