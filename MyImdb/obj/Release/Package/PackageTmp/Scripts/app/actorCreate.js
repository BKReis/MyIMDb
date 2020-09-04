app.controller('actorCreateController', ['$scope', '$http', '$location', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, blockUI, notifyService, lib) {
    $scope.actor = {
        name: '',
        birthplace: ''
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Actors', $scope.actor).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    //$location.url('/Genre');
                    window.location.assign('/Actor');
                    notifyService.success('Actor created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };
}]);