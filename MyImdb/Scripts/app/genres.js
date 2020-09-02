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

        }, lib.handleError);
    };

    var openfavoritegenredialog = function () {
        var modalinstance = $uibmodal.open({
            templateurl: '/dialog/favoritegenre',
            controller: 'favoritegenredialogcontroller',
            resolve: {
                items: function () {
                    return $scope.genres;
                }
            }
        });
        modalinstance.result.then(function (msg) {
        }, function (msg) {//dismiss callback
        });
    };

    init();
}]);