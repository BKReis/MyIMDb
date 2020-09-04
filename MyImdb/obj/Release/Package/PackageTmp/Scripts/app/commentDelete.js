app.controller('commentDeleteController', ['$scope', '$http', '$location', '$routeParams', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, $routeParams, blockUI, notifyService, lib) {
    $scope.comment = {
        id: window.location.pathname.split('/')[3],
        selectedMovieId: ''
    };

    var init = function () {
        //Initial code
        blockUI.start();
        loadComment();
        blockUI.stop();
    };

    var loadComment = function () {
        $http.get('/Api/Comments/' + $scope.comment.id).then(function (response) {
            $scope.comment = response.data;
            console.log($scope.comment)
        }, lib.handleError);
    };

    $scope.delete = function () {
        blockUI.start();
        $http.delete('/Api/Comments/' + $scope.comment.id).then(function (response) {
            blockUI.stop();
            setTimeout(function () {
                window.location.assign('/Movie/Details/' + $scope.comment.selectedMovieId);
                notifyService.success('Comment deleted with success.');
            }, 1500);
        }, function (r) {
            lib.handleError(r, form);
        });
    }

    init();
}]);