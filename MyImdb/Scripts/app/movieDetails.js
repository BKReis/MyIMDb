app.controller('movieDetailsController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movie = {
        id: window.location.pathname.split('/')[3],
        title: '',
        rank: '',
        storyline: '',
        year: '',
        selectedGenreId: '',
        actors: []
    };

    var init = function () {
        blockUI.start();
        loadMovie();
        blockUI.stop();
    };

    var loadMovie = function () {

        $http.get('/Api/Movies/' + $scope.movie.id + '/Detailed').then(function (response) {
            $scope.movie = response.data;
        }, lib.handleError);
    };

    init();
}]);