using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using Clientele.Training.Contracts;
using Clientele.Training.Persistence;
using Clientele.Training.WebApi.Models;
using Clientele.Training.WebApi.Queries;
using Hermes.Messaging.Bus;


namespace Clientele.Training.WebApi.Controllers
{
    public class TrainingTopicController : ApiController
    {
        private readonly TopicQueryService topicQueryService;
        private readonly LocalBus localBus;

        public TrainingTopicController(LocalBus localBus, TopicQueryService queryService)
        {
            this.localBus = localBus;
            this.topicQueryService = queryService;
        }

      
        [HttpGet]
        public HttpResponseMessage GetTopic(Guid topicId)
        {
            try
            {
                var traingingTopic = topicQueryService.Get(topicId);

                if (traingingTopic == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, traingingTopic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]

        public HttpResponseMessage GetTrainingTopicsPerCategory(Guid categoryId)
        {

            try
            {
              var categoryTrainingTopics = topicQueryService.GetTrainingTopicsByCategoryId(categoryId);
               return Request.CreateResponse(HttpStatusCode.OK, categoryTrainingTopics);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
  
        
        [HttpPost]
        public HttpResponseMessage PostTrainingTopic(TrainingTopicDto trainingTopic)
        {
            try
            {
                var id = SequentialGuid.New();
                var createTrainingTopic = new CreateTrainingTopic(id, trainingTopic.Name, trainingTopic.Context, trainingTopic.CategoryId);
                localBus.Execute(createTrainingTopic);
                var response = Request.CreateResponse(HttpStatusCode.Created, trainingTopic);

                string uri = Url.Link("DefaultApi", new { id = createTrainingTopic.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutTrainingTopic(TrainingTopicDto trainingTopic)
        {
            try
            {
                var updateTrainingTopic = new UpdateTrainingTopic(trainingTopic.Id, trainingTopic.Name, trainingTopic.Context, trainingTopic.CategoryId);
                localBus.Execute(updateTrainingTopic);
                
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteTrainingTopic(Guid topicId)
        {
            try
            {
                var deleteTrainingTopic = new DeleteTrainingCategory(topicId);
                localBus.Execute(deleteTrainingTopic);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}