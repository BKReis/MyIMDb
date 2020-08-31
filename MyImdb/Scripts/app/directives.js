angular.module('lacunaDirectives', [])
 .directive('srvError', ['$compile',
  function ($compile) {
      var linker = function (scope, element, attrs) {
          var fieldName = attrs.srvError;
          var formName = attrs.form;

          if (formName === undefined || formName === null) {
              //find form name
              formName = $(element).closest('form').attr('name');

              if (formName === undefined || formName === null) {
                  console.error('Could not find form name for srvError ' + fieldName);
              }
          }

          var serverErrorMessage = formName + '.' + fieldName + '.$error.message';

          var clone = $(element).clone();

          clone.addClass('help-block');
          clone.attr('ng-show', serverErrorMessage);
          clone.html('{{' + serverErrorMessage + '}}');
          clone.removeAttr('srv-error');//remove directive to avoid infinite loop of recompilation
          var strElem = '';
          strElem = $("<div />").append(clone).html();
          $(element).replaceWith($compile(strElem)(scope));
      }

      return {
          restrict: 'A',
          link: linker
      }
  }])
.directive('error', ['$compile',
  function ($compile) {
      var linker = function (scope, element, attrs) {
          var fieldName = attrs.error;
          var formName = attrs.form;

          if (formName === undefined || formName === null) {
              //find form name
              formName = $(element).closest('form').attr('name');

              if (formName === undefined || formName === null) {
                  console.error('Could not find form name for error ' + fieldName);
              }
          }

          var errorType = attrs.type;

          if (errorType === undefined || errorType === null) {
              console.error('Please provide a type for error ' + fieldName + ' in form ' + formName);
          }

          var showErrorOn = formName + '.' + fieldName + '.$error.' + errorType;

          if (attrs.beforesubmit !== 'true') {
              showErrorOn += ' && ' + formName + '.$submitted';
          }

          var clone = $(element).clone();

          clone.addClass('help-block');
          clone.attr('ng-show', showErrorOn);
          clone.removeAttr('error');//remove directive to avoid infinite loop of recompilation
          var strElem = '';
          strElem = $("<div />").append(clone).html();
          $(element).replaceWith($compile(strElem)(scope));
      }

      return {
          restrict: 'A',
          link: linker
      }
  }])
.directive('formGroup', ['$compile',
  function ($compile) {
      var linker = function (scope, element, attrs) {
          var fieldName = attrs.formGroup;
          var formName = attrs.form;

          if (formName === undefined || formName === null) {
              //find form name
              formName = $(element).closest('form').attr('name');

              if (formName === undefined || formName === null) {
                  console.error('Could not find form name for error ' + fieldName);
              }
          }

          $(element).addClass('form-group');
          $(element).removeAttr('form-group');

          var watchFunc = function (newValue, oldValue) {
              var val = scope[formName][fieldName].$invalid;
              if (attrs.beforesubmit !== 'true' ? val && scope[formName].$submitted : val) {
                  $(element).addClass('has-error');
              } else {
                  $(element).removeClass('has-error');
              }
          };

          scope.$watch(function (scope) {
              return scope[formName][fieldName].$invalid;
          }, watchFunc);

          scope.$watch(function (scope) {
              return scope[formName].$submitted;
          }, watchFunc);
      }

      return {
          restrict: 'A',
          link: linker
      }
  }])

.directive('lacDatepicker', function () {
    var linker = function (scope, element, attrs, ngModel) {
        var format = attrs.lacDatepickerFormat;
        if (!format) {
            format = 'dd/mm/yyyy';
        }

        var viewMode = scope.$eval(attrs.lacDatepickerViewmode);

        var start = scope.$eval(attrs.lacDatepickerStartDate);
        scope.$watch(attrs.lacDatepickerStartDate, function (newVal, oldValue) {
            if (newVal === oldValue) {
                return;
            }
            element.datepicker('setStartDate', newVal);
            if (!scope.$$phase) {
                scope.$apply();
            }
        });

        var end = scope.$eval(attrs.lacDatepickerEndDate);
        scope.$watch(attrs.lacDatepickerEndDate, function (newVal, oldValue) {
            if (newVal === oldValue) {
                return;
            }
            element.datepicker('setEndDate', newVal);
            if (!scope.$$phase) {
                scope.$apply();
            }
        });

        element.datepicker({
            language: 'pt-BR',
            todayHighlight: true,
            todayBtn: 'linked',
            format: format,
            autoclose: true,
            minViewMode: viewMode,
            startDate: start,
            endDate: end
        })
        .on('changeDate', function (e) {
            $(this).trigger("input");
        });

        scope.$watch(attrs.lacDatepickerViewmode, function (newVal, oldValue) {
            if (newVal === oldValue) {
                return;
            }
            element.datepicker('remove');
            element.datepicker({
                language: 'pt-BR',
                todayHighlight: true,
                todayBtn: 'linked',
                format: format,
                autoclose: true,
                minViewMode: newVal,
                startDate: start,
                endDate: end
            })
            .on('changeDate', function (e) {
                $(this).trigger("input");
            });
        });

        var actualDate = scope.$eval(attrs.ngModel);
        if (actualDate !== undefined && actualDate !== null && actualDate.length > 0) {
            element.datepicker('update', actualDate);
            if (!scope.$$phase) {
                scope.$apply();
            }
        }
    };
    return {
        restrict: 'EA',
        require: '?ngModel',
        link: linker
    };
})
.directive('lacunaUploader', function () {
    var controller = ['$scope', '$http', '$timeout', '$translate', 'httpInterceptor', 'lib', 'notifyService', function ($scope, $http, $timeout, $translate, httpInterceptor, lib, notifyService) {
        
        $scope.initController = function () {
            $scope.state = 'init';
            $scope.progress = 0;
            $timeout(initUploader, 500);
        };

        var requestUpload = function (up, file) {
            $http.get('/Upload').then(function (response) {
                file.uploadTicket = response.data.ticket;
                up.settings.chunk_size = response.data.chunkSize;
                up.start();
            }, lib.handleError);
        };

        var initUploader = function () {

            // authorize
            var config = httpInterceptor.request({ headers: {} });

            $scope.uploader = new plupload.Uploader({
                runtimes: 'gears,html5,flash,silverlight,browserplus,html4',
                browse_button: 'uploadButton',
                container: 'uploadContainer',
                unique_names: false,
                multi_selection: false,
                max_file_size: '10mb',
                chunk_size: '2mb',
                url: '/Upload',
                headers: config.headers,
                flash_swf_url: '/Content/plupload/Moxie.swf',
                silverlight_xap_url: '/Content/plupload/Moxie.xap',
                multipart_params: {
                },
                filters: [
                ],
                init: {
                    FilesAdded: function (up, files) {
                        var file = files[0];
                        $scope.upload = {
                            fileName: file.name,
                            mimeType: file.type,
                            size: file.size
                        };
                        if ($scope.onStartUpload) {
                            $scope.onStartUpload({ file: $scope.upload });
                        }
                        $scope.state = 'uploading';
                        $scope.$apply();
                        up.refresh();
                        requestUpload(up, file);
                    },
                    UploadProgress: function (up, file) {
                        $scope.upload.size = file.size;
                        $scope.progress = file.percent;
                        $scope.$apply();
                    },
                    FileUploaded: function (up, file, response) {
                        response.data = angular.fromJson(response.response);

                        $scope.upload.targetName = file.target_name;
                        $scope.upload.key = response.data.key;
                        $scope.$apply();

                        $translate('upSuccess').then(function (msg) {
                            notifyService.success(msg);
                        });

                        if ($scope.onUpload) {
                            $scope.onUpload({ file: $scope.upload });
                        }

                        $timeout(readyToUploadAgain, 1000);
                    },
                    BeforeUpload: function (up, file) {
                        up.settings.multipart_params.ticket = file.uploadTicket;
                    },
                    Error: function (up, err) {
				        notifyService.error(err.message);
				    }
                }
            });

            $scope.uploader.init();
            $scope.state = 'ready';
        };

        var readyToUploadAgain = function () {
            $scope.state = 'ready';
            $scope.progress = 0;
            $scope.$apply();
        };
    }];
    var linker = function (scope, element, attrs, controller) {
        scope.initController();
    };
    return {
        restrict: 'E',
        scope: { onUpload: '&onUpload', onStartUpload: '&onStartUpload' },
        templateUrl: '/app/views/template/uploadButton.html',
        controller: controller,
        link: linker
    }
})
;
