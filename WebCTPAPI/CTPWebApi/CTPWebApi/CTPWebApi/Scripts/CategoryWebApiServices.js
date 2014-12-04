(function () {


    var CategoryWebApiServices= function ($http) {

        return {

            GetCategory: function () {
                return $http.get('api/category');
            },

            AddCategory: function (addedCategory) {
                return $http.post('api/category', addedCategory);
            },

            RemoveCategory: function (deletedCategoryId) {
                return $http.delete('api/category/?categoryid=' + deletedCategoryId);
            },

            EditCategory: function (editedCategory) {
                return $http.put('api/category', editedCategory);
            },

            GetCategoryPagingList: function (page, itemsPerPage) {

                return $http.get('api/Category/PagingList/?page=' + page + '&itemsPerPage=' + itemsPerPage);
            }
        };
    };

    CategoryWebApiServices.$inject = ['$http'];

    angular.module('CategoryWebApiServices', []).factory('CategoryWebApiServices', CategoryWebApiServices);
})();