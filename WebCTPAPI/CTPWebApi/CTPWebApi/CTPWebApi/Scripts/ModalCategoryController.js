(function () {

    var ModalCategoryController = function ($scope, $modalInstance) {


        $scope.AddCategory = function (gategory) {
            $modalInstance.close(gategory);
        };

        $scope.Cancel = function () {
            $modalInstance.dismiss('cancel');
        };

    };

    ModalCategoryController.$inject = ['$scope', '$modalInstance'];
    angular.module('ModalCategoryController',[]).controller('ModalCategoryController', ModalCategoryController);

})();