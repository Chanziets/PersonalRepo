using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using RepositoryUnitOfWorkPatterns.Models;
using RepositoryUnitOfWorkPatterns.Queries;

namespace RepositoryUnitOfWorkPatterns.Controllers
{
    public class TrainingCategoryController : ApiController
    {
        private TrainingUnitOfWork unitOfWork = new TrainingUnitOfWork();
        private readonly CategoryQueryService categoryQueryService;

        public TrainingCategoryController(CategoryQueryService categoryQueryService)
        {
            this.categoryQueryService = categoryQueryService;
        }
  
        public IEnumerable<TrainingCategory> GetAllTrainingCategories()
        {
            return categoryQueryService.GetAllTrainingCategories();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid categoryId)
        {
            try
            {
                TrainingCategory traingingCategory = categoryQueryService.Get(categoryId);
                if (traingingCategory == null)
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
        [Route("api/TrainingCategory/GetCategoryTopicReport")]
        public HttpResponseMessage GetCategoryTopicReport()
        {
            try
            {
                IEnumerable<TrainingCategoryTopicReport> trainingCategoryTopicReports = categoryQueryService.GetCategoryTopicReport();

                if (trainingCategoryTopicReports == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, trainingCategoryTopicReports);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostTrainingCategory(TrainingCategory trainingCategory)
        {
            try
            {
                // This logic maybe in the repository with it getting an extra method.
                trainingCategory.CategoryId = Guid.NewGuid();
                trainingCategory.DateAdded = DateTime.Now;

                unitOfWork.TrainingCategoryRepository.Insert(trainingCategory);
                unitOfWork.Save();
                var response = Request.CreateResponse(HttpStatusCode.Created, trainingCategory);

                string uri = Url.Link("DefaultApi", new { id = trainingCategory.CategoryId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutTrainingCategory(TrainingCategory trainingCategory)
        {
            try
            {
                unitOfWork.TrainingCategoryRepository.Update(trainingCategory);
                unitOfWork.Save();
                //if (!trainingcategoryRepository.Update(trainingCategory))
                //{
                //    return new HttpResponseMessage(HttpStatusCode.NotModified);
                //}

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
                TrainingCategory trainingCategory = unitOfWork.TrainingCategoryRepository.GetById(tc => tc.CategoryId == categoryId);
                if (trainingCategory == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category not found.");
                }

                unitOfWork.TrainingCategoryRepository.Delete(trainingCategory);
                unitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}