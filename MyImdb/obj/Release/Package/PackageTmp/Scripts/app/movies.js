app.controller('moviesController', ['$scope', '$http', '$uibModal', 'blockUI', 'notifyService', 'lib', '$filter', 'NgTableParams', function ($scope, $http, $uibModal, blockUI, notifyService, lib, $filter, NgTableParams) {

    var createTable = function (options, dataFunction) {
        return new NgTableParams(options, {
            total: dataFunction().length,
            getData: function ($defer, params) {
                console.log(params)
                var orderedData = params.sorting() ? $filter('orderBy')(dataFunction(), params.orderBy()) : dataFunction();
                orderedData = params.filter() ? $filter('filter')(orderedData, params.filter()) : orderedData;
                params.total(orderedData.length);
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
    };

    var movies = [];
    //$scope.moviesTable = lib.createTable({ page: 1, count: 10, sorting: { title: 'asc' } }, function () { return movies; });
    $scope.moviesTable = createTable({ page: 1, count: 10, sorting: { title: 'asc' } }, function () { return movies; });


    var init = function () {
        //Initial code
        loadMovies();
    };

    var loadMovies = function () {
        blockUI.start();
        $http.get('/Api/Movies').then(function (response) {
            blockUI.stop();
            movies = response.data;
            $scope.moviesTable.reload();
        }, lib.handleError);
    };

    init();
    }]);
