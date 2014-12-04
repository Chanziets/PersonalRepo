(function () {


    var TopicWebApiServices = function ($http, $upload) {

        return {

            GetTopic: function () {
                return $http.get('api/topic');
            },

            GetTopicsPerCategory: function (categoryId) {
                return $http.get('api/topic/?categoryId=' + categoryId);
            },

            TopicsPerCategorySearch: function (categoryTopicSearch) {
                var queryTopicSearch = JSON.stringify(categoryTopicSearch);
                return $http.get('api/Topic/TopicSearch/?CategoryTopicSearch=' + queryTopicSearch);
            },

            AddTopic: function (addedTopic) {
                return $http.post('api/topic', addedTopic);
            },

            RemoveTopic: function (deletedTopicId) {
                return $http.delete('api/topic/?topicid=' + deletedTopicId);
            },

            EditTopic: function (editedTopic) {
                return $http.put('api/topic', editedTopic);
            },

            UpdateTopicHierarchy: function ( topicList) {
                return $http.put('api/Topic/Ordering/', topicList);
            },

            AddTopicAttachment: function (attachmentFile, topicId) {
             
                var fileUploadObj = { topicAttachmentId: topicId };
                return $upload.upload({
                    url: 'api/Topic/PostTopicAttachment/',
                    method: "POST",
                    data: fileUploadObj ,
                    file : attachmentFile
                    });
            },

            GetTopicAttachment: function(topicId) {
                return $http.get('api/Topic/GetTopicAttachments/?topicId=' + topicId);
            }

        };
    };

    TopicWebApiServices.$inject = ['$http', '$upload'];

    angular.module('TopicWebApiServices', []).factory('TopicWebApiServices', TopicWebApiServices);
})();