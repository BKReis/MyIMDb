app.controller('movieEditController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movie = {
        id: window.location.pathname.split('/')[3],
        title: '',
        rank: '',
        storyline: '',
        year: '',
        selectedGenreId: ''
    };

    $scope.genres = [];

    var init = function () {
        blockUI.start();
        loadMovie();
        loadGenres();
        blockUI.stop();
    };

    var loadMovie = function () {
      
        $http.get('/Api/Movies/' + $scope.movie.id + '/Detailed').then(function (response) {
           
            $scope.movie = response.data;
            $scope.placeHolder = response.data.genre;
            console.log($scope.movie);
        }, lib.handleError);
    };

    var loadGenres = function () {
        $http.get('/Api/Genres').then(function (response) {
            $scope.genres = response.data;
        }, lib.handleError);
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.put('/Api/Movies/' + $scope.movie.id, $scope.movie).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie');
                    notifyService.success('Movie edited with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    $scope.selectGenre = function (genreId, genreName) {

        $scope.placeHolder = genreName;
        $scope.movie.selectedGenreId = genreId;
    };
    init();
}]);