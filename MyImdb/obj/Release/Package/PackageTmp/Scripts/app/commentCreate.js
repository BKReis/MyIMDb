app.controller('commentCreateController', ['$scope', '$http', 'blockUI', 'notifyService', 'lib', function ($scope, $http, blockUI, notifyService, lib) {
    $scope.comment = {
        value: '',
        selectedMovieId: window.location.pathname.split('/')[3]
    };


    var init = function () {
        blockUI.start();
        blockUI.stop();
    };

    $scope.save = function (form) {

        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Comments', $scope.comment).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie/Details/' + $scope.comment.selectedMovieId);
                    notifyService.success('Character created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);