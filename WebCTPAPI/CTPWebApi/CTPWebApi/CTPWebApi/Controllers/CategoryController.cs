using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using CTPWebApi.Models;

namespace CTPWebApi.Controllers
{
    public class CategoryController : ApiController
    {
        static readonly ICategoryRepository categories = new CategoryRepository();

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return categories.GetAllCategories();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid categoryId)
        {
            try
            {
                Category category = categories.Get(categoryId);
                if (category == null)
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
        [Route("api/Category/PagingList/")]
        public HttpResponseMessage GetPagingCategoryList(int page, int itemsPerPage)
        {
            try
            {
                IEnumerable<CategoryDto> pagingCategoryList = categories.GetPagingListCategories(page, itemsPerPage);

                if (pagingCategoryList == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, pagingCategoryList);
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
                item = categories.Add(item);
                var response = Request.CreateResponse(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.CategoryId });
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
                if (!categories.Update(item))
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
                Category item = categories.Get(categoryId);
                if (item == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category not found.");
                }

                categories.Remove(categoryId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}