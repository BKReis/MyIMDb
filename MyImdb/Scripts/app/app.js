var app = angular.module('myIMDBApp', ['ngRoute', 'blockUI', 'lacunaDirectives', 'ui.bootstrap', 'ngTable']);
app.config(['blockUIConfig', function (blockUIConfig) {
    blockUIConfig.autoBlock = false;
    blockUIConfig.message = 'Please wait ...';
}]);

app.service('notifyService', ['$rootScope', function ($rootScope) {
    PNotify.prototype.options.styling = "bootstrap3";
    PNotify.prototype.options.delay = 4000;
    PNotify.prototype.options.width = '400px';
    //STACK OPTIONS
    var stack_center = { "dir1": "down", "dir2": "right", "firstpos1": 25, "firstpos2": ($(window).width() / 2) - (Number(PNotify.prototype.options.width.replace(/\D/g, '')) / 2) };
    $(window).resize(function () {
        stack_center.firstpos2 = ($(window).width() / 2) - (Number(PNotify.prototype.options.width.replace(/\D/g, '')) / 2);
    });
    var notify = function (title, type, text) {
        var opts = {
            addclass: "stack-bar-top",
            stack: stack_center,
            text: text,
            type: type
        };
        new PNotify(opts);
    }
    this.success = function (msg) {
        notify('', 'success', msg)
    };
    this.error = function (msg) {
        notify('', 'error', msg)
    };
    this.warning = function (msg) {
        notify('', 'warning', msg)
    };
    this.info = function (msg) {
        notify('', 'info', msg)
    };
}]);