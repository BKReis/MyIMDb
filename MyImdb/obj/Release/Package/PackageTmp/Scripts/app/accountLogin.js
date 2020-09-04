app.controller('accountLoginController', ['$scope', '$http', '$location', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, blockUI, notifyService, lib) {
    $scope.login = {
        email: '',
        password: '',
        rememberMe: false
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Account/Login', $scope.login).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie');
                    notifyService.success('Logged in with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };
}]);