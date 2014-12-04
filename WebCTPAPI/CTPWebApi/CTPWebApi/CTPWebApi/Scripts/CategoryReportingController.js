(function () {

    var CategoryReportingController = function ($scope, CategoryTopicReportService, $window) {

        var loadCategoryDetails = function() {

            CategoryTopicReportService.GetCategoryTopicReport().
                success(function(data) {
                    $scope.CategoryTopicReport = data;
                }).
                error(function(response) {
                    $window.alert(response.Message);
                });

            $scope.gridCategories = {
                data: 'CategoryTopicReport',
                showColumnMenu: true,
                columnDefs: [
                    {
                        field: 'CategoryName',
                        displayName: 'Category',
                        enableCellEdit: false
                    },
                    {
                        field: 'LinkedTopicCount',
                        displayName: 'Linked Topics',
                        enableCellEdit: false
                    }
                ]
            }
        };

        loadCategoryDetails();
    };

    CategoryReportingController.$inject = ['$scope', 'CategoryTopicReportService', '$window'];

    angular.module('CategoryReportingController', ['CategoryTopicReportService']).controller('CategoryReportingController', CategoryReportingController);

})();