app.controller('genresController', ['$scope', '$http', '$uibModal','blockUI','notifyService', 'lib', function ($scope, $http, $uibModal, blockUI, notifyService, lib) {
    $scope.genres = [];
    var init = function () {
        //Initial code
        loadGenres();
    };
    var loadGenres = function () {
        var eblock = blockUI.instances.get('genresContentDiv');
        eblock.start();
        $http.get('/Api/Genres').then(function (response) {
            eblock.stop();
            $scope.genres = response.data;
            openFavoriteGenreDialog();
        }, lib.handleError);
        //}, function (response) {
        //    eblock.stop();
        //        alert('Error loading genres.');
        //        notifyService.error('Error loading genres.');
        //});
    };

    var openFavoriteGenreDialog = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Dialog/FavoriteGenre',
            controller: 'favoriteGenreDialogController',
            resolve: {
                items: function () {
                    return $scope.genres;
                }
            }
        });
        modalInstance.result.then(function (msg) {
        }, function (msg) {//dismiss callback
        });
    };

    init();
}]);