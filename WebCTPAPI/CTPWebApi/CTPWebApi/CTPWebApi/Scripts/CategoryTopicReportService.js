(function() {

    var CategoryTopicReportService = function($http) {

        return {
            GetCategoryTopicReport: function() {
                return $http.get('api/categorytopicreport');
            }
        }
    };

    CategoryTopicReportService.$inject = ['$http'];

    angular.module('CategoryTopicReportService', []).factory('CategoryTopicReportService', CategoryTopicReportService);

})();