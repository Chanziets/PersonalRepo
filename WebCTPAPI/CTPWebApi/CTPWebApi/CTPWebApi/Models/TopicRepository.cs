using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using CTPWebApi.Models.InputDtos;

namespace CTPWebApi.Models
{
    public class TopicRepository : ITopicRepository
    {
        private CtpWebContext db = new CtpWebContext();

        private int nextTopicId = 1;
        private int nextPriorityWeight = 1;
        private DateTime defaultdate = DateTime.Now;


        private IQueryable<TopicDto> MapTopics()
        {
            return from t in db.Topic
                   select new TopicDto() { TopicId = t.TopicId, TopicName = t.TopicName, TopicContext = t.TopicContext, PriorityWeight = t.Order, DateAdded = t.DateAdded, CategoryId = t.CategoryId};
        }

        public IEnumerable<TopicDto> GetAllTopics()
        {
            return MapTopics().AsEnumerable();
        }

        public Topic Get(Guid topicId)
        {
            var topic = db.Topic.FirstOrDefault(t => t.TopicId == topicId);
            return topic;
        }

        public IEnumerable<Topic> GetTopicPerCategory(Guid categoryid)
        {

             var topics = db.Topic.Where(t => t.CategoryId == categoryid).OrderBy(t => t.Order).ToList();
             return topics;

        }

        public IEnumerable<Topic> GetTopicPerCategorySearch(List<CategoryTopicSearch> CategoryTopicSearch)
        {
            var topics = new List<Topic>();

            CategoryTopicSearch.ForEach(c =>
            {
                var topicssearched =
                    db.Topic.Where(t => t.CategoryId == c.CategoryId && t.TopicName.Contains(c.SearchTopic))
                        .OrderBy(t => t.Order)
                        .ToList();

                topics.AddRange(topicssearched);
            });

            return topics;

        }

        public TopicDto Get(string topicName)
        {
            var topic = (from t in MapTopics() where t.TopicName == topicName select t).FirstOrDefault();
            return topic;
        }

        public TopicDto Add(TopicDto topic)
        {

            if (topic == null)
            {
                throw new ArgumentNullException("topic");
            }

            TopicDto topicdto = Get(topic.TopicName);
            Topic newtopic = new Topic();

            if (topicdto == null)
            {
                //Automated Values
                newtopic.TopicId = Guid.NewGuid();
                newtopic.DateAdded = defaultdate;
                newtopic.Order = nextPriorityWeight++;
                //Submitted Values
                newtopic.TopicName = topic.TopicName;
                newtopic.TopicContext = topic.TopicContext;
            }
            else
            {
                throw new Exception("Topic already exists.");
            }

            db.Topic.Add(newtopic);
            db.SaveChanges();

            topic.TopicId = newtopic.TopicId;
            topic.PriorityWeight = newtopic.Order;
            return topic;
        }

        public void Remove(Guid topicId)
        {
            Topic topic = (from t in db.Topic where t.TopicId == topicId select t).FirstOrDefault();
            db.Topic.Remove(topic);
            db.SaveChanges();
        }

        public bool Update(TopicDto topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException("topic");
            }

            TopicDto topicDto = Get(topic.TopicName);
            Topic editedTopic = db.Topic.Find(topic.TopicId);

            if (editedTopic != null)
            {
                if (topicDto == null || topicDto.TopicName == editedTopic.TopicName)
                {
                    //Submitted Values
                    editedTopic.TopicContext = topic.TopicContext;
                    editedTopic.TopicName = topic.TopicName;
                }
                else
                {
                    throw new Exception("Topic with submitted new topic name already exists.");
                }
            }
            else
            {
                throw new Exception("Topic not found.");
            }

            db.SaveChanges();
            return true;
        }

        public bool UpdateTopicHierarchy(List<ObjectOrderingDto> topiclist)
        {
            if (topiclist == null)
            {
                throw new ArgumentNullException("topiclist");
            }

            foreach (var topic in topiclist)
            {
                Topic editedTopic = db.Topic.Find(topic.ObjectId);

                if (editedTopic != null)
                {
                    editedTopic.Order = topic.Order;
                }
            }

            db.SaveChanges();
            return true;

        }

        public IEnumerable<TopicAttachment> GetTopicAttachments(Guid topicId)
        {
            var topicAttachments = db.TopicAttachment.Where(t => t.TopicId == topicId).ToList();
            return topicAttachments;
        }

        public TopicAttachment AddTopicAttachment(TopicAttachment topicAttachment)
        {

            if (topicAttachment == null)
            {
                throw new ArgumentNullException("topicAttachment");
            }

            TopicAttachment newTopicAttachment = new TopicAttachment();

            newTopicAttachment.TopicAttachmentId = Guid.NewGuid();
            newTopicAttachment.DateAdded = defaultdate;
            newTopicAttachment.TopicAttachmentFile = topicAttachment.TopicAttachmentFile;
            newTopicAttachment.TopicAttachmentFileName = topicAttachment.TopicAttachmentFileName;
            newTopicAttachment.TopicId = topicAttachment.TopicId; 
            db.TopicAttachment.Add(newTopicAttachment);
            db.SaveChanges();

            return newTopicAttachment;
        }
    }
}