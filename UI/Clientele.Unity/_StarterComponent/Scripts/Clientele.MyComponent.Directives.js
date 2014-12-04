(function () {

    var applicationId = "Clientele.MyComponent",
        configuration = applicationHost.retrieveApplicationConfigurationById(applicationId),
        sourceUrl = configuration.UnityUrl + "Views/",
        buildNumber = configuration.BuildNumber;

    var myComponent = function () {
        return {
            restrict: 'E',
            templateUrl: sourceUrl + 'Template/Test.html?buildNumber=' + buildNumber,
            scope: {
                policyNumber: '=?',
                loadEvent: '=?',
            },
            controller: 'myComponentController',
        };
    };

    angular.module('Clientele.MyComponent.Directives', [])
        .directive('myComponent', myComponent);
})();