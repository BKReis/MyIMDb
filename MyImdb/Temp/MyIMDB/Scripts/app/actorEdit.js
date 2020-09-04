app.controller('actorEditController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.actor = {
        id: window.location.pathname.split('/')[3],
        name: '',
        birthplace: ''
    };

    var init = function () {
        //Initial code
        loadActor();
    };

    var loadActor = function () {
        var eblock = blockUI.instances.get('actorsContentDiv');
        eblock.start();
        $http.get('/Api/Actors/' + $scope.actor.id).then(function (response) {
            eblock.stop();
            $scope.actor = response.data;
            console.log($scope.actor);
            console.log(response.data);
        }, lib.handleError);
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.put('/Api/Actors/' + $scope.actor.id, $scope.actor).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Actor');
                    notifyService.success('Actor edited with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);