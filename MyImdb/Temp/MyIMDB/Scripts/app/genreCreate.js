app.controller('genreCreateController', ['$scope', '$http', '$location', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, blockUI, notifyService, lib) {
    $scope.genre = {
        name: ''
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Genres', $scope.genre).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    //$location.url('/Genre');
                    window.location.assign('/Genre');
                    notifyService.success('Genre created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };
}]);