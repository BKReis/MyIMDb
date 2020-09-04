app.controller('genreDeleteController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.genre = {
        id: window.location.pathname.split('/')[3],
        name: ''
    };

    $scope.movies = [];

    var init = function () {
        //Initial code
        blockUI.start();
        loadGenre();
        loadMovieTitles();
        blockUI.stop();
    };

    var loadMovieTitles = function () {
        $http.get('/Api/Genres/' + $scope.genre.id + '/MovieTitles').then(function (response) {
            //eblock.stop();
            $scope.movies = response.data;
        }, lib.handleError);
    };

    var loadGenre = function () {
        //eblock.start();
        $http.get('/Api/Genres/' + $scope.genre.id).then(function (response) {
            //eblock.stop();
            $scope.genre = response.data;
        }, lib.handleError);
    };

    $scope.delete = function () {
        blockUI.start();
        $http.delete('/Api/Genres/' + $scope.genre.id).then(function (response) {
            blockUI.stop();
            setTimeout(function () {
                window.location.assign('/Genre');
                notifyService.success('Genre deleted with success.');
            }, 1500);
        }, function (r) {
            lib.handleError(r, form);
        });
    }

    init();
}]);