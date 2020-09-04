app.controller('favoriteGenreDialogController', ['$scope', '$http', '$uibModal', '$uibModalInstance', 'blockUI', 'notifyService', 'lib', 'items', function ($scope, $http, $uibModal, $uibModalInstance, blockUI, notifyService, lib, items) {
    $scope.genres = [];
    var init = function () {
        //Initial code
        $scope.genres = items
        console.log("AAAAAAAAAAAAAA");
        if ($scope.genres.length == 0) {
            console.log("Entrou no if")
            loadGenres();
        } 
    };
    var loadGenres = function (genres) {
        var eblock = blockUI.instances.get('genresContentDiv');
        eblock.start();
        $http.get('/Api/Genres').then(function (response) {
            eblock.stop();
            $scope.genres = response.data;
        }, lib.handleError);
        //}, function (response) {
        //    eblock.stop();
        //        alert('Error loading genres.');
        //        notifyService.error('Error loading genres.');
        //});
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    init();
}]);