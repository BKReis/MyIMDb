app.controller('genreCreateController', ['$scope', '$http', 'blockUI', 'notifyService', 'lib', function ($scope, $http, blockUI, notifyService, lib) {
    $scope.genre = {
        name: ''
    };

    var clearvalidationerrors = function (form) {
        for (name in form) {
            if (name.substring(0, 1) !== '$') {
                form[name].$error = {};
                form[name].$invalid = false;
            }
        }
    };
    var setvalidationerrors = function (form, data) {
        $.each(data, function (index, valor) {
            try {
                form[valor.field].$error = { message: valor.error };
                form[valor.field].$setvalidity('server', true);
            } catch (ex) {
                console.error('error for ' + valor + ' field: ' + valor.field);
                console.error(ex);
            }
        });
    };

    $scope.save = function (form) {
        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Genres', $scope.genre).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    notifyService.success('Genre created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
            //}, function (response) {
            //    blockUI.stop();
            //    clearValidationErrors(form);
            //    if (response.status == 400) {
            //        if (response.data.validationErrors != null) {
            //            setValidationErrors(form, response.data.validationErrors);
            //            return;
            //        }
            //    }
            //    notifyService.error(response.data.message);
            //});
        }
    };
}]);