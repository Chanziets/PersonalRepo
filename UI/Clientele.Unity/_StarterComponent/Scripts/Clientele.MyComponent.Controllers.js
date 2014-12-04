(function () {

    var applicationId = "Clientele.MyComponent",
        configuration = applicationHost.retrieveApplicationConfigurationById(applicationId),
        sourceUrl = configuration.UnityUrl + "Views/",
        buildNumber = configuration.BuildNumber;

    var myComponentIndexController = function ($scope, myComponentApi) {
        var privateVar = "test data";

        var privateFunction = function () {
            alert("private");
        }

        $scope.publicVar = "scoped data";

        $scope.publicFunction = function () {
            alert("public");
        }

        var loadAction = function () {
            alert("Your view content was loaded and this function called");
        }

        $scope.$on("$viewContentLoaded", loadAction);
    };

    var myComponentController = function ($scope) {


    };

    var testController = function ($scope) {


    };

    var myComponentSearchController = function ($scope, $routeParams) {

        var loadAction = function () {
            $scope.searchedText = $routeParams.searchText;
        }

        $scope.$on("$viewContentLoaded", loadAction);

    };

    myComponentIndexController.$inject = ['$scope', 'myComponentApi'];
    myComponentSearchController.$inject = ['$scope', '$routeParams'];
    testController.$inject = ['$scope'];

    angular.module('Clientele.MyComponent.Controllers', ['Clientele.MyComponent.Services'])
        .controller('MyComponentSearchController', myComponentSearchController)
        .controller('MyComponentController', myComponentController)
        .controller('TestController', testController)
        .controller('MyComponentIndexController', myComponentIndexController);
})();
