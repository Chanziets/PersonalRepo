(function () {
    // function - factory type service
    var myComponentApi = function (ajaxJsonService, myComponentApiUrl) {
        return {
            GetSomeObjectTypes: function () {
                var url = myComponentApiUrl + "ObjectType";
                return ajaxJsonService.Get(url, null);
            },
            GetSomeObjectTypeById: function (id) {
                var url = myComponentApiUrl + "ObjectType/" + id;
                return ajaxJsonService.Get(url, null);
            },
            CreateNewObjectType: function (objectType) {
                var url = myComponentApiUrl + "ObjectType";
                return ajaxJsonService.Post(url, objectType);
            },
            ChangeObjectTypeValue: function (id, newValues) {
                var url = myComponentApiUrl + "ObjectType/updateSomeValue/" + id;
                return ajaxJsonService.Put(url, newValues);
            },
            DeleteObjectTypeValue: function (id) {
                var url = myComponentApiUrl + "ObjectType/" + id;
                return ajaxJsonService.Delete(url, null);
            }
        };
    };

    var someSpecialLookupDataThatIsSharedButChangeIsAlmostNever = function () {
        var selectListOne = [];
        selectListOne.push({ Label: "Mrs", Value: 0 });
        selectListOne.push({ Label: "Mr", Value: 1 });
        selectListOne.push({ Label: "Ms", Value: 2 });

        return {
            SelectListOne: selectListOne
        };
    };

    myComponentApi.$inject = ['ajaxJsonService', 'myComponentApiUrl'];
    someSpecialLookupDataThatIsSharedButChangeIsAlmostNever.$inject = [];

    angular.module('Clientele.MyComponent.Services', [])
        .factory('myComponentApi', myComponentApi)
        .factory('someSpecialLookupDataThatIsSharedButChangeIsAlmostNever', someSpecialLookupDataThatIsSharedButChangeIsAlmostNever);
}());