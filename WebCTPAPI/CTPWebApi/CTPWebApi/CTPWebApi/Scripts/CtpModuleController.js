(function () {

    var CtpModuleController = function ($scope, $window, CategoryWebApiServices, TopicWebApiServices, $timeout, $modal) {

        $scope.MultipleSelect = {};
        $scope.MultipleSelect.categories = [];
        $scope.TopicsSelected = undefined;
        $scope.TopicsSelected3 = undefined;
        $scope.Time = "";

        var SetTime = function () {
            $scope.Time = Date.now();
            $timeout(SetTime, 1000);
        }

        $timeout(SetTime, 1000);

        $scope.SetSubView = function (routeView, viewAction) {
            $scope.Subview = routeView;
            $scope.SubviewAction = viewAction;
        }

        $scope.TemplateUrl = function () {
            switch ($scope.Subview) {
                case "ViewTopic":
                    return 'CtpTopicContentDisplay.html';
                case "AddTopic":
                    return 'CtpTopicAddEdit.html';
                case "EditTopic":
                    return 'CtpTopicAddEdit.html';
                case "ManageCategories":
                    return 'CtpAdmin.html';
                default:
                    return '';
            }
        }

        $scope.$on("$SetSubView", function (event, routeView) {
            $scope.SetSubView(routeView);
        });

        //User Rights
        $scope.UserWriteRights = true;

        //Categories
        $scope.CategoryName = "Select Category";
        $scope.TopicName = "Select Topic";

        var getCategoryList = function () {
            CategoryWebApiServices.GetCategory().
            success(function (data) {
                $scope.categories = data;
            }).
            error(function (response) {
                $window.alert(response.Message);
            });
        }

        $scope.$on("$viewContentLoaded", getCategoryList);


        //Topics
        $scope.topics = [];


        var getTopicList = function (categoryId) {
            TopicWebApiServices.GetTopicsPerCategory(categoryId)
                .success(function (data) {
                    $scope.topics = data;
                    $scope.SetSubView("Default", "None");
                }).
                error(function (response) {
                    $window.alert(response.Message);
                });
        }

        $scope.GetTopicContext = function (arrayindex) {
            var selectedTopic = $scope.topics[arrayindex];
            $scope.SetSubView("ViewTopic", "viewTopic");
            $scope.topic = selectedTopic;
            $scope.TopicName = selectedTopic.TopicName;
        }

        $scope.getTopicsPerCategory = function (categoryId) {
            TopicWebApiServices.GetTopicsPerCategory(categoryId)
            .success(function (data) {
                data.forEach(function (object) {
                    $scope.topics.push(object);
                });
                if ($scope.TopicsSelected == "") {
                    $scope.SetSubView("Default", "None");
                }

            }).
            error(function (response) {
                $window.alert(response.Message);
            });
        }



        $scope.getTopicsPerCategorySearch = function (searchTopic) {

            var categoryTopicSearch = [];

            for (var i = 0; i < $scope.MultipleSelect.categories.length; i++) {
                var categoryId = $scope.MultipleSelect.categories[i].CategoryId;
                categoryTopicSearch.push({ "CategoryId": categoryId, "SearchTopic": searchTopic });
                
            }

            return TopicWebApiServices.TopicsPerCategorySearch(categoryTopicSearch)
                .then(function(response) {
                    return response.data;
                });
        }

        $scope.$watch('MultipleSelect.categories', function (newCategory, oldCategory) {

            if (newCategory.length < oldCategory.length) {
                return;
            }

            if ($scope.MultipleSelect.categories.length > 0) {
                var max = newCategory.length - 1;
                var categoryId = newCategory[max].CategoryId;
                $scope.getTopicsPerCategory(categoryId);
            }

        }, true);

        $scope.removecategorytopics = function (removedCategory) {

            for (var i = 0; i < $scope.topics.length; i++) {

                if ($scope.topics[i].CategoryId == removedCategory.CategoryId) {
                    $scope.topics.splice(i, 1);
                    i--;
                }

            }

            if ($scope.TopicsSelected.CategoryId == removedCategory.CategoryId) {
                $scope.TopicsSelected = "";
                $scope.SetSubView("Default", "None");
            }
        }

        var updateTopic = function (editedtopic) {
            TopicWebApiServices.EditTopic(editedtopic).
             success(function () {
                 getTopicList(editedtopic.CategoryId);
             }).
             error(function (response) {
                 $window.alert(response.Message);
             });
        }

        var addTopic = function (addedtopic) {
            TopicWebApiServices.AddTopic(addedtopic).
              success(function (data) {
                  $scope.topics.push(data);
                  addedtopic.AreaName = "";
                  addedtopic.AreaContext = "";
              }).
              error(function (response) {
                  $window.alert(response.Message);
              });
        }

        $scope.$on("$addTopic", function (event, topic) {
            addTopic(topic);
        });

        $scope.$on("$editTopic", function (event, topic) {
            updateTopic(topic);
        });

        $scope.$watch('TopicsSelected', function (newTopic, oldTopic) {
            if (newTopic != oldTopic && (typeof newTopic) != "string") {
                $scope.SetSubView("ViewTopic", "viewTopic");
                $scope.topic = newTopic;
                $scope.TopicName = newTopic.TopicName;
            } else if ((typeof newTopic) == "string") {
                $scope.SetSubView("Default", "None");
            }
        }, true);


        //$scope.OpenAddCategory = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: 'AddCategory.html',
        //        controller: 'ModalCategoryController',
        //        resolve: {
        //            category: function () {
        //                return $scope.category;
        //            }
        //        }
        //    });

        //    modalInstance.result.then(function (category) {
        //        addCategory(category);
        //    });
        //}


    };

    CtpModuleController.$inject = ['$scope', '$window', 'CategoryWebApiServices', 'TopicWebApiServices', '$timeout', '$modal'];

    angular.module('CtpModuleController', ['CategoryWebApiServices', 'TopicWebApiServices']).controller('CtpModuleController', CtpModuleController);
})();







