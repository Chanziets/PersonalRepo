using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using CTPWebApi.Models;
using CTPWebApi.Models.InputDtos;
using Newtonsoft.Json;


namespace CTPWebApi.Controllers
{

    public class TopicController : ApiController
    {

        static readonly ITopicRepository topics = new TopicRepository();


        public IEnumerable<TopicDto> GetAllTopics()
        {
            return topics.GetAllTopics();
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid topicId)
        {
            try
            {
                Topic topic = topics.Get(topicId);

                if (topic == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, topic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Topic/GetTopicAttachments/")]
        public HttpResponseMessage GetTopicAttachments(Guid topicId)
        {
            try
            {
                IEnumerable<TopicAttachment> topicAttachments = topics.GetTopicAttachments(topicId);

                return Request.CreateResponse(HttpStatusCode.OK, topicAttachments);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetTopicsPerCategory(Guid categoryId)
        {
            try
            {
                var categoryTopics = topics.GetTopicPerCategory(categoryId);

                if (categoryTopics == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, categoryTopics);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Topic/TopicSearch/")]
        public HttpResponseMessage TopicPerCategorySearch(string CategoryTopicSearch)
        {
            try
            {
                var searchQuery = JsonConvert.DeserializeObject<List<CategoryTopicSearch>>(CategoryTopicSearch);

                List<CategoryTopicSearch> categorySearchList = searchQuery;

                var categoryTopics = topics.GetTopicPerCategorySearch(categorySearchList);
                
                if (categoryTopics == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, categoryTopics);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        public HttpResponseMessage PostTopic(TopicDto item)
        {
            try
            {
                item = topics.Add(item);
                var response = Request.CreateResponse(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.TopicId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage PutTopic(TopicDto item)
        {
            try
            {
                if (!topics.Update(item))
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

        [HttpPut]
        [Route("api/Topic/Ordering/")]
        public HttpResponseMessage PutTopicOrder(List<ObjectOrderingDto> topicList)
        {
            try
            {

                if (!topics.UpdateTopicHierarchy(topicList))
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

        [HttpPost]
        [Route("api/Topic/PostTopicAttachment/")]
        public async Task<HttpResponseMessage> PostTopicAttachment()
        {
            try
            {

                if (!Request.Content.IsMimeMultipartContent())
                {
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                TopicAttachment newTopicAttachment = new TopicAttachment();

                newTopicAttachment.TopicAttachmentFileName = GetDeserializedFileName(result.FileData.First());
                newTopicAttachment.TopicId = GetTopicID(result);
                byte[] file = File.ReadAllBytes(result.FileData.First().LocalFileName);
                newTopicAttachment.TopicAttachmentFile = file;

                TopicAttachment topicAttachment = topics.AddTopicAttachment(newTopicAttachment);

                return Request.CreateResponse(HttpStatusCode.OK, topicAttachment);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = fileData.Headers.ContentDisposition.FileName;
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        private Guid GetTopicID(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData =
                    Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return Guid.Parse(unescapedFormData);
            }

            return Guid.Empty;
        }

        [HttpDelete]
        public HttpResponseMessage DeleteTopic(Guid topicId)
        {
            try
            {
                Topic item = topics.Get(topicId);
                if (item == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Topic not found.");
                }

                topics.Remove(topicId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
