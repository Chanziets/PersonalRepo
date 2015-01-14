using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DynamicWebAPI.Models;
using DynamicWebAPI.Repositories;

namespace DynamicWebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();

        public IEnumerable<object> GetAllCategories()
        {
            return categoryRepository.GetAllCategories();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid categoryId)
        {
            try
            {
                var category = categoryRepository.Get(categoryId);
                if (category == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage PostCategory(CategoryDto item)
        {
            try
            {
                CategoryDto category = categoryRepository.Add(item);
                var response = Request.CreateResponse(HttpStatusCode.Created, category);

                string uri = Url.Link("DefaultApi", new { id = category.CategoryId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutCategory(CategoryDto item)
        {
            try
            {
                if (!categoryRepository.Update(item))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotModified);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCategory(Guid categoryId)
        {
            try
            {
                object item = categoryRepository.Get(categoryId);
                if (item == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category not found.");
                }

                categoryRepository.Remove(categoryId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
