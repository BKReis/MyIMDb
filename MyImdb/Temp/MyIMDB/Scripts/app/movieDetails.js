app.controller('movieDetailsController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movie = {
        id: window.location.pathname.split('/')[3],
        title: '',
        rank: '',
        storyline: '',
        year: '',
        selectedGenreId: '',
        actors: [],
        comments: []
    };

    $scope.user = {
        id: '',
        username: ''
    }

    var init = function () {
        blockUI.start();
        loadMovie();
        loadCurrentUser();
        blockUI.stop();
    };

    var loadMovie = function () {

        $http.get('/Api/Movies/' + $scope.movie.id + '/Detailed').then(function (response) {
            $scope.movie = response.data;
            console.log(response.data)
            console.log($scope.movie);
        }, lib.handleError);
    };

    var loadCurrentUser = function () {
        $http.get('/Api/Account/Info').then(function (response) {
            $scope.user = response.data;
            console.log(response.data)
            console.log($scope.user);
        }, lib.handleError);
    };

    init();
}]);