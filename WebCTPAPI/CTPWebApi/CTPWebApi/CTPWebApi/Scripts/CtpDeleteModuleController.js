(function () {

    var CtpDeleteModuleContoller = function ($scope, $window, CategoryWebApiServices, TopicWebApiServices) {

        $scope.RemoveCategory = function (arrayindex) {
            var delCategory = $scope.categories[arrayindex];

            CategoryWebApiServices.RemoveCategory(delCategory.CategoryId).
                success(function () {
                    $scope.categories.splice(arrayindex, 1);
                    $scope.$emit('$resetTopics');
                    $scope.$emit('$resetContext');
                }).
                error(function (response) {
                    $window.alert(response.Message);
                });
        }

        $scope.RemoveTopic = function (arrayindex) {
            var delTopic = $scope.topics[arrayindex];

            TopicWebApiServices.RemoveTopic(delTopic.TopicId).
                success(function () {
                    $scope.topics.splice(arrayindex, 1);
                    $scope.$emit('$resetContext');
                }).
                error(function (response) {
                    $window.alert(response.Message);
                });
        }

    };

    CtpDeleteModuleContoller.$inject = ['$scope', '$window', 'CategoryWebApiServices', 'TopicWebApiServices'];

    angular.module('CtpDeleteModuleContoller', ['CategoryWebApiServices', 'TopicWebApiServices']).controller('CtpDeleteModuleContoller', CtpDeleteModuleContoller);

})();