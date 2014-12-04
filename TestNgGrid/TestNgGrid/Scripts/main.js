var app = angular.module('GridApp', ['ngGrid', 'ngSanitize', 'ui.select']);
app.controller('GridController', function ($scope) {

    $scope.myData = [{ name: "Moroni", age: 50 },
                     { name: "Tiancum", age: 43 },
                     { name: "Jacob", age: 27 },
                     { name: "Nephi", age: 29 },
                     { name: "Enos", age: 34 }];

    $scope.gridOptions = { data: 'myData' };

    $scope.availableColors = ['Red', 'Green', 'Blue', 'Yellow', 'Magenta', 'Maroon', 'Umbra', 'Turquoise'];

    $scope.multipleDemo = {};
    $scope.multipleDemo.colors = ['Blue', 'Red'];

});