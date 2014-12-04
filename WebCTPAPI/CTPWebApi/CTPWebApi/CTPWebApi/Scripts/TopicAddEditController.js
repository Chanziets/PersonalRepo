(function () {

    var TopicAddEditController = function ($scope, TopicWebApiServices, $window) {

        $scope.ButtonAction = "Add";
        $scope.SelectCategoryName = "Select Category";
        $scope.FileUrl = "";

        var FormSetup = function () {

            $scope.FormCategoriesList = angular.copy($scope.categories);

            if ($scope.SubviewAction == "editTopic") {
                $scope.FormTopic = angular.copy($scope.topic);
                var category = findSelectedCategory($scope.FormCategoriesList, $scope.FormTopic.CategoryId);
                $scope.SelectCategoryName = category.CategoryName;
                $scope.ButtonAction = "Update";
                loadTopicAttachments();
            } else {
                $scope.ButtonAction = "Add";
            }

        }

        var loadTopicAttachments = function () {
            TopicWebApiServices.GetTopicAttachment($scope.FormTopic.TopicId)
                 .success(function (data) {
                     $scope.TopicAttachments = data;
                 }).
                  error(function (response) {
                      $window.alert(response.Message);
                  });
        }

        $scope.SetCategoryId = function (arrayIndex) {
            var selectedCategory = $scope.FormCategoriesList[arrayIndex];
            $scope.SelectCategoryName = selectedCategory.CategoryName;
            $scope.FormTopic.CategoryId = selectedCategory.CategoryId;//Issue on Add
        }

        var findSelectedCategory = function (categoryList, selectedObjectId) {
            for (var i = 0; i < categoryList.length; i++) {
                if (categoryList[i].CategoryId === selectedObjectId) {
                    return categoryList[i];
                }
            }
        }

        $scope.SetPrevView = function () {
            if ($scope.SubviewAction == "editTopic") {
                $scope.$emit("$SetSubView", 'ViewTopic');
            } else {
                $scope.$emit("$SetSubView", 'Default');
            }
        }

        $scope.$watch(function () { return $scope.ButtonAction; },
            function (newValue) {
                var actionButton = document.getElementById('AddEditTopicButton'); //Find angular way of doing it.
                //var actionButton = angular.element('#AddEditTopicButton');
                actionButton.value = newValue;
            });

        $scope.AddSaveTopic = function () {
            if ($scope.SubviewAction == "editTopic") {
                $scope.$emit("$editTopic", $scope.FormTopic);
                $scope.$emit("$SetSubView", 'ViewTopic');
            } else {
                $scope.$emit("$addTopic", $scope.FormTopic);
                $scope.$emit("$SetSubView", 'Default');
            }
        }

        $scope.onFileSelect = function ($files) {

            var topicId = $scope.FormTopic.TopicId;

            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                TopicWebApiServices.AddTopicAttachment($file, topicId)
                    .success(function (data) {
                        $scope.TopicAttachments.push(data);
                    }).
                    error(function (response) {
                        $window.alert(response.Message);
                    });
            }

        }

        $scope.GetUrl = function (arrayIndex) {
            var filecontent = decodeURIComponent(escape(window.atob($scope.TopicAttachments[arrayIndex].TopicAttachmentFile)));
            var blob = new Blob([filecontent], { type: 'text/plain' });
            var url = $window.URL || $window.webkitURL;
            $scope.FileUrl = url.createObjectURL(blob);
        }

        $scope.TestText = "Test text";
        $scope.CustomMenu = [
        ['bold', 'italic', 'underline', 'strikethrough', 'subscript', 'superscript'],
        ['font'],
        ['font-size'],
        ['font-color', 'hilite-color'],
        ['remove-format'],
        ['ordered-list', 'unordered-list', 'outdent', 'indent'],
        ['left-justify', 'center-justify', 'right-justify'],
        ['code', 'quote', 'paragragh'],
        ['link', 'image']
        ];

        FormSetup();
    }

    TopicAddEditController.$inject = ['$scope', 'TopicWebApiServices', '$window'];

    angular.module('TopicAddEditController', ['TopicWebApiServices']).controller('TopicAddEditController', TopicAddEditController);

})();