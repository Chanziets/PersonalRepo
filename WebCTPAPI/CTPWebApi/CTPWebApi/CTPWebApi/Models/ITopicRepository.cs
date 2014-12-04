using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTPWebApi.Models.InputDtos;

namespace CTPWebApi.Models
{
    public interface ITopicRepository
    {
        IEnumerable<TopicDto> GetAllTopics();
        Topic Get(Guid topicId);
        TopicDto Add(TopicDto item);
        IEnumerable<Topic> GetTopicPerCategory(Guid categoryId);
        IEnumerable<Topic> GetTopicPerCategorySearch(List<CategoryTopicSearch> categoryTopicSearch); 
        void Remove(Guid topicId);
        bool Update(TopicDto item);
        bool UpdateTopicHierarchy(List<ObjectOrderingDto> topiclist);
        TopicAttachment AddTopicAttachment(TopicAttachment topicAttachment);
        IEnumerable<TopicAttachment> GetTopicAttachments(Guid topicId);
    }
}
