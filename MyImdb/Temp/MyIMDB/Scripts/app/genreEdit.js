app.controller('genreEditController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.genre = {
        id: window.location.pathname.split('/')[3],
        name: ''
    };

    var init = function () {
        //Initial code
        blockUI.start();
        loadGenre();
        blockUI.stop();
    };

    var loadGenre = function () {
        $http.get('/Api/Genres/' + $scope.genre.id).then(function (response) {
            $scope.genre = response.data;
        }, lib.handleError);
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.put('/Api/Genres/' + $scope.genre.id, $scope.genre).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Genre');
                    notifyService.success('Genre edited with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);