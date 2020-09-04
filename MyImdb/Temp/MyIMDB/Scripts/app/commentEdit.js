app.controller('commentEditController', ['$scope', '$http', 'blockUI', 'notifyService', 'lib', function ($scope, $http, blockUI, notifyService, lib) {
    $scope.comment = {
        id: window.location.pathname.split('/')[3],
        value: '',
        selectedMovieId: ''
    };

    var init = function () {
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

    $scope.save = function (form) {

        if (form.$valid) {
            blockUI.start();
            $http.put('/Api/Comments/' + $scope.comment.id, $scope.comment).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie/Details/' + $scope.comment.selectedMovieId);
                    notifyService.success('Comment updated with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);