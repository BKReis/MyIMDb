app.factory('lib', ['$http', '$rootScope', 'blockUI', 'notifyService', function ($http, $rootScope, blockUI, notifyService) {
    var createTable = function (options, dataFunction) {
        return new NgTableParams(options, {
            total: dataFunction().length,
            getData: function ($defer, params) {
                console.log(params)
                var orderedData = params.sorting() ? $filter('orderBy')(dataFunction(), params.orderBy()) : dataFunction();
                orderedData = params.filter() ? $filter('filter')(orderedData, params.filter()) : orderedData;
                params.total(orderedData.length);
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
    };

    var handleError = function (response, form) {
        blockUI.stop();
        if (form != null) {
            clearValidationErrors(form);
        }
        if (response.status == 400) {
            if (response.data.validationErrors != null && form != null) {
                setValidationErrors(form, response.data.validationErrors);
                return;
            } else if (response.data.generalError != null) {
                notifyService.warning(response.data.generalError);
                return;
            } else if (response.data.error_description != null) { // oauth authentication errors
                notifyService.warning(response.data.error_description);
                return;
            }
        }
        if (response.data.message) {
            notifyService.error(response.data.message);
        } else {
            notifyService.error('An unspecified error has ocurred.');
        }
    };
    var clearValidationErrors = function (form) {
        for (name in form) {
            if (name.substring(0, 1) !== '$') {
                form[name].$error = {};
                form[name].$invalid = false;
            }
        }
    };
    var setValidationErrors = function (form, data) {
        $.each(data, function (index, valor) {
            try {
                form[valor.field].$error = { message: valor.error };
                form[valor.field].$setValidity('server', true);
            } catch (ex) {
                console.error('Error for ' + valor + ' field: ' + valor.field);
                console.error(ex);
            }
        });
    };
    return {
        handleError: handleError,
        clearValidationErrors: clearValidationErrors,
        setValidationErrors: setValidationErrors
    };
}]);