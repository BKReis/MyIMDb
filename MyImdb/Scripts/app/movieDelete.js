app.controller('movieDeleteController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movie = {
        id: window.location.pathname.split('/')[3],
        title: '',
        rank: '',
        storyLine: '',
        year: '',
        selectedGenreId: ''
    };

    $scope.characters = [];

    var init = function () {
        blockUI.start();
        loadMovie();
        loadCharacters();
        blockUI.stop();
    };

    var loadMovie = function () {
        
        $http.get('/Api/Movies/' + $scope.movie.id).then(function (response) {
        
            $scope.movie = response.data;
        }, lib.handleError);
    };

    var loadCharacters = function () {
        $http.get('/Api/Movies/' + $scope.movie.id + '/Characters').then(function (response) {
            //eblock.stop();
            $scope.characters = response.data;
        }, lib.handleError);
    };

    $scope.delete = function () {
        blockUI.start();
        $http.delete('/Api/Movies/' + $scope.movie.id).then(function (response) {
            blockUI.stop();
            setTimeout(function () {
                window.location.assign('/Movie');
                notifyService.success('Movie deleted with success.');
            }, 1500);
        }, function (r) {
            lib.handleError(r, form);
        });
    }

    init();
}]);