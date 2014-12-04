(function () {

    var ManageTabsController = function ($scope) {

        $scope.tabs = [
        {
            name: 'Categories',
            url: 'CategoriesManage.html',
            active1: true
        }, {
            name: 'Category Topic Hierarchy',
            url: 'CategoryTopicHierarchy.html',
            active1: false
        }
        , {
            name: 'Category Reporting',
            url: 'CategoryReporting.html',
            active1: false
        }
        ];

        $scope.tab = 'CategoriesManage.html';
        $scope.current = 'Categories';

        $scope.toggleTab = function (tab) {
            $scope.tab = tab.url;
            $scope.current = tab.name;
        };
        
    }

    ManageTabsController.$inject = ['$scope'];
    angular.module('ManageTabsController', []).controller('ManageTabsController', ManageTabsController);
})();