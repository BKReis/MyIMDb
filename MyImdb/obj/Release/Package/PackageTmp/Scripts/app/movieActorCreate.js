app.controller('movieActorCreateController', ['$scope', '$http', 'blockUI', 'notifyService', 'lib', function ($scope, $http, blockUI, notifyService, lib) {
    $scope.movieActor = {
        character: '',
        selectedActorId: '',
        selectedMovieId: window.location.pathname.split('/')[3]
    };
    $scope.placeHolder = "Select the actor";

    $scope.actors = [];

    var init = function () {
        blockUI.start();
        loadActors();
        blockUI.stop();
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
            $http.post('/Api/MovieActors', $scope.movieActor).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie');
                    notifyService.success('Character created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);