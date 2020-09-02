app.controller('movieActorEditController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.movieActor = {
        id: window.location.pathname.split('/')[3],
        character: '',
        selectedActorId: '',
        selectedMovieId: ''
    };

    $scope.actors = [];

    var init = function () {
        blockUI.start();
        loadMovieActor();
        loadActors();
        blockUI.stop();
    };

    var loadMovieActor = function () {
        $http.get('/Api/MovieActors/' + $scope.movieActor.id).then(function (response) {
            $scope.movieActor = response.data;
            $scope.placeHolder = response.data.name;

        }, lib.handleError);
    };

    var loadActors = function () {
        $http.get('/Api/Actors').then(function (response) {
            $scope.actors = response.data;
        }, lib.handleError);
    };

    $scope.selectActor = function (actorId, actorName) {

        $scope.placeHolder = actorName;
        $scope.movieActor.selectedActorId = actorId;
    };

    $scope.save = function (form) {

        if (form.$valid) {
            blockUI.start();
            $http.put('/Api/MovieActors/' + $scope.movieActor.id, $scope.movieActor).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie');
                    notifyService.success('Character edited with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);