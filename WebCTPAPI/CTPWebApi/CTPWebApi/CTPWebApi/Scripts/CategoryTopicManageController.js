(function () {

    var CategoryTopicManageController = function ($scope, TopicWebApiServices, $window) {

        var formSetup = function () {

            $scope.SelectCategoryName = "Select Category";
            $scope.OderingFeedback = "";
            $scope.HierArchyCategoriesList = angular.copy($scope.categories);
        }

        $scope.GetTopics = function (arrayIndex) {

            var selectedTopics = $scope.HierArchyCategoriesList[arrayIndex];

            TopicWebApiServices.GetTopicsPerCategory(selectedTopics.CategoryId)
            .success(function (data) {
                $scope.topics = data;
            }).
            error(function (response) {
                $window.alert(response.Message);
            });


            $scope.SelectCategoryName = selectedTopics.CategoryName;
            $scope.OderingFeedback = "";
        }

        $scope.topics = [];
        $scope.DisableEditTopicOrder = true;

        $scope.SelectedTopics = {
            selected: null,
            lists: $scope.topics
        };

        $scope.updateTopicOrder = function ($index) {
            $scope.topics.splice($index, 1);
        }

        $scope.DisableEditCategoryTopicsOrder = function (disableEditing) {
            $scope.DisableEditTopicOrder = disableEditing;
        }

        $scope.SaveTopicOrder = function () {

            var topicNewHierarchy = [];
            var newOrder = 1;

            for (var i = 0; i < $scope.topics.length; i++) {

                topicNewHierarchy.push({ "Order": newOrder, "ObjectId": $scope.topics[i].TopicId });
                newOrder++;
            }

            TopicWebApiServices.UpdateTopicHierarchy( topicNewHierarchy)
            .success(function () {
                    $scope.OderingFeedback = "New topic order updated succesfully";
                }).
            error(function (response) {
                $window.alert(response.Message);
            });

            $scope.DisableEditCategoryTopicsOrder(true);

        }

        formSetup();
    };

    CategoryTopicManageController.$inject = ['$scope', 'TopicWebApiServices', '$window'];

    angular.module('CategoryTopicManageController', ['TopicWebApiServices']).controller('CategoryTopicManageController', CategoryTopicManageController);

})();