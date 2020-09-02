app.controller('actorDeleteController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.actor = {
        id: window.location.pathname.split('/')[3],
        name: '',
        birthplace: ''
    };

    $scope.characters = [];

    var init = function () {
        //Initial code
        blockUI.instances.get('actorsContentDiv');
        blockUI.start();
        loadActor();
        loadCharacters();
        blockUI.stop();
    };

    var loadCharacters = function () {
        $http.get('/Api/Actors/' + $scope.actor.id + '/Characters').then(function (response) {
            //eblock.stop();
            $scope.characters = response.data;
        }, lib.handleError);   
    };

    var loadActor = function () {
        //eblock.start();
        $http.get('/Api/Actors/' + $scope.actor.id).then(function (response) {
            //eblock.stop();
            $scope.actor = response.data;
        }, lib.handleError);
    };

    $scope.delete = function () {
            blockUI.start();
            $http.delete('/Api/Actors/' + $scope.actor.id).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Actor');
                    notifyService.success('Actor deleted with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
    }

    init();
}]);