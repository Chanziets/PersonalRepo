(function () {

    var CategoryManageController = function ($scope, Pagination, CategoryWebApiServices, $window) {

        $scope.pagination = Pagination.getNew();
        $scope.CategoryPagingList = [];
        $scope.pageNumber = $scope.pagination.page;

        var loadCategoryPagingList = function(page, itemsPerPage) {

            CategoryWebApiServices.GetCategoryPagingList(page, itemsPerPage)
                .success(function(data) {
                    $scope.CategoryPagingList = data;
                }).error(function(response) {
                    $window.alert(response.Message);
                });

        };

        $scope.pagination.numPages = Math.ceil($scope.categories.length / $scope.pagination.perPage);

        $scope.HideAddCategoryForm = true;
        $scope.UserWriteRights = true;

        $scope.HideCategoryAddForm = function (hideCategoryAddFormValue) {
            $scope.HideAddCategoryForm = hideCategoryAddFormValue;
        }

        $scope.AddCategory = function (newCategory) {
            CategoryWebApiServices.AddCategory(newCategory).
              success(function (data) {
                  $scope.categories.push(data);
                  $scope.CategoryPagingList.push(data);
                  newCategory.CategoryName = "";
                  $scope.HideCategoryAddForm(true);
                  $scope.pagination.numPages = Math.ceil($scope.categories.length / $scope.pagination.perPage);
              }).
              error(function (response) {
                  $window.alert(response.Message);
              });
        }

        var editCategory = function (category) {
            CategoryWebApiServices.EditCategory(category).
            success(function () {
            }).
            error(function (response) {
                $window.alert(response.Message);
            });
        }

        $scope.$on("$editCategory", function (event, category) {
            editCategory(category);
        });

        $scope.GetCategoryPagingList = function (pageIncrement) {
            
            loadCategoryPagingList($scope.pagination.page + (pageIncrement), 5);
        }

        $scope.GetCategoryPagingListPage = function (nextPage) {

            loadCategoryPagingList(nextPage, 5);
        }

        loadCategoryPagingList($scope.pagination.page, 5);
    };

    CategoryManageController.$inject = ['$scope', 'Pagination', 'CategoryWebApiServices', '$window'];

    angular.module('CategoryManageController', ['Pagination', 'CategoryWebApiServices']).controller('CategoryManageController', CategoryManageController);

})();