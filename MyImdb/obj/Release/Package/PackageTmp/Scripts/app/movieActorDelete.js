app.controller('movieActorDeleteController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movieActor = {
        id: window.location.pathname.split('/')[3],
        selectedActorId: '',
        selectedMovieId: ''
    };

    var init = function () {
        //Initial code
        blockUI.start();
        loadMovieActor();
        blockUI.stop();
    };

    var loadMovieActor = function () {
        $http.get('/Api/MovieActors/' + $scope.movieActor.id).then(function (response) {
            $scope.movieActor = response.data;
        }, lib.handleError);
    };

    $scope.delete = function () {
        blockUI.start();
        $http.delete('/Api/MovieActors/' + $scope.movieActor.id).then(function (response) {
            blockUI.stop();
            setTimeout(function () {
                window.location.assign('/Movie');
                notifyService.success('Character deleted with success.');
            }, 1500);
        }, function (r) {
            lib.handleError(r, form);
        });
    }

    init();
}]);