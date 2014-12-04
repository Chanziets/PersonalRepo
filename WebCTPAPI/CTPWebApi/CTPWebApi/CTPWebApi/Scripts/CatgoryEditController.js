(function () {

    var CategoryEditController = function ($scope) {

        var loadCategories = function () {
            $scope.$emit("$viewContentLoaded");
        }

        $scope.$on("$viewContentLoaded", loadCategories);

        $scope.ValidateCategory = function (categoryname) {

            if (categoryname == null) {
                return 'Please enter category name.';
            } else if ($scope.category.CategoryName == categoryname) {
                return false;
            }
        };

        $scope.EditCategory = function () {
            $scope.$emit("$editCategory", $scope.category);
        };

    };

    CategoryEditController.$inject = ['$scope'];
    angular.module('CategoryEditController', []).controller('CategoryEditController', CategoryEditController);

})();