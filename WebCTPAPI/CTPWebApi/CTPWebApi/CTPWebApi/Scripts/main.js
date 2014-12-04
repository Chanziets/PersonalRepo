(function () {

    angular.module('CTPApp', [
        'CtpModuleController', 'CtpDeleteModuleContoller', 'ModalCategoryController', 'CategoryEditController', 'TopicAddEditController', 'ManageTabsController', 'CategoryManageController',
        'CategoryTopicManageController', 'CategoryReportingController', 'ngRoute', 'ui.bootstrap', 'xeditable', 'ngGrid', 'ngSanitize', 'ui.select', 'dndLists', 'Pagination', 'angularFileUpload','textAngular'])
        .config([
            '$routeProvider', function($routeProvider) {

                $routeProvider.when('/', {
                    templateUrl: 'CtpRealEstateView.html',
                    controller: 'CtpModuleController'
                });
            }
        ])
        .config(['$compileProvider', function($compileProvider) {
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(|blob|):/);
        }])
        .run(function(editableOptions) {
            editableOptions.theme = 'bs3';
        });
   
})();