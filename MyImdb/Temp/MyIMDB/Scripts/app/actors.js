app.controller('actorsController', ['$scope', '$http', '$uibModal', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $uibModal, blockUI, notifyService, lib) {
    $scope.actors = [];
    var init = function () {
        //Initial code
        loadActors();
    };
    var loadActors = function () {
        var eblock = blockUI.instances.get('actorsContentDiv');
        eblock.start();
        $http.get('/Api/Actors').then(function (response) {
            eblock.stop();
            $scope.actors = response.data;

        }, lib.handleError);
    };

    init();
}]);