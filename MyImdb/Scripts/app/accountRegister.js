app.controller('accountRegisterController', ['$scope', '$http', '$location', 'blockUI', 'notifyService', 'lib', function ($scope, $http, $location, blockUI, notifyService, lib) {
    $scope.register = {
        email: '',
        password: ''
    };

    $scope.passwordConfirmation = '';

    $scope.save = function (form) {
        //TODO Check if the passwordConfirmation matches the password
        if (form.$valid) {
            console.log("entrou")
            blockUI.start();
            $http.post('/Api/Account/Register', $scope.register).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    //$location.url('/Genre');
                    window.location.assign('/Home/Login');
                    notifyService.success('Registered with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };
}]);