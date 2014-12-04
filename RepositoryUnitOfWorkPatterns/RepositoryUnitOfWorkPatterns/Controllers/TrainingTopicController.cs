using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using RepositoryUnitOfWorkPatterns.Models;
using RepositoryUnitOfWorkPatterns.Queries;
using RepositoryUnitOfWorkPatterns.Repositories;

namespace RepositoryUnitOfWorkPatterns.Controllers
{
    public class TrainingTopicController : ApiController
    {
        private readonly TopicQueryService topicQueryService;
        private TrainingUnitOfWork unitOfWork = new TrainingUnitOfWork();

        public TrainingTopicController(TopicQueryService queryService)
        {
            this.topicQueryService = queryService;
        }

        public List<TrainingTopic> GetAllTrainingTopics()
        {
            return topicQueryService.GetAllTrainingTopics();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid topicId)
        {
            try
            {
                TrainingTopic traingingTopic = topicQueryService.Get(topicId);
                if (traingingTopic == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
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
        public HttpResponseMessage PostTrainingTopic(TrainingTopic trainingTopic)
        {
            try
            {
                //This logic maybe in the repository with it getting an extra method.
                trainingTopic.TopicId = Guid.NewGuid();
                trainingTopic.DateAdded = DateTime.Now;

                unitOfWork.TrainingTopicRepository.Insert(trainingTopic);
                var response = Request.CreateResponse(HttpStatusCode.Created, trainingTopic);

                string uri = Url.Link("DefaultApi", new { id = trainingTopic.TopicId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutTrainingTopic(TrainingTopic trainingTopic)
        {
            try
            {
                unitOfWork.TrainingTopicRepository.Update(trainingTopic);
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
                TrainingTopic trainingTopic = unitOfWork.TrainingTopicRepository.GetById(tc => tc.TopicId == topicId);
                if (trainingTopic == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Topic not found.");
                }

                unitOfWork.TrainingTopicRepository.Delete(trainingTopic);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}