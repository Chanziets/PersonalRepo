(function () {

    var applicationId = "Clientele.MyComponent",
        configuration = applicationHost.retrieveApplicationConfigurationById(applicationId),
        sourceUrl = configuration.UnityUrl + "Views/",
        buildNumber = configuration.BuildNumber;

    var myComponentRoutes = function ($routeProvider) {
        var componentKey = "MyComponent",
            componentUrlPrefix = "/" + componentKey + "/";

        $routeProvider
            .when(componentUrlPrefix, { templateUrl: sourceUrl + 'Index.html?buildNumber=' + buildNumber, controller: 'MyComponentIndexController', caseInsensitiveMatch: true })
            .when(componentUrlPrefix + "Test", { templateUrl: sourceUrl + 'Test.html?buildNumber=' + buildNumber, controller: 'TestController', caseInsensitiveMatch: true })
            .when(componentUrlPrefix + "UnitySearch/:searchText", { templateUrl: sourceUrl + 'Search.html?buildNumber=' + buildNumber, controller: 'MyComponentSearchController', caseInsensitiveMatch: true });
    };

    myComponentRoutes.$inject = ['$routeProvider'];

    angular.module('Clientele.MyComponent.Routes', ['Clientele.MyComponent.Controllers'])
        .config(myComponentRoutes);
})();