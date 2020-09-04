app.controller('movieCreateController', ['$scope', '$http', 'blockUI', 'notifyService', 'lib', function ($scope, $http, blockUI, notifyService, lib) {
    $scope.movie = {
        title: '',
        rank: '',
        year: '',
        storyLine: '',
        selectedGenreId: ''
    };
    $scope.placeHolder = "Select your movie genre";

    $scope.genres = [];

    var init = function () {
        loadGenres();
    };

    var loadGenres = function () {
        var eblock = blockUI.instances.get('genresContentDiv');
        eblock.start();
        $http.get('/Api/Genres').then(function (response) {
            eblock.stop();
            $scope.genres = response.data;
        }, lib.handleError);
    };

    $scope.selectGenre = function (genreId, genreName) {

        $scope.placeHolder = genreName;
        $scope.movie.selectedGenreId = genreId;
    };

    $scope.save = function (form) {

        if (form.$valid) {
            blockUI.start();
            $http.post('/Api/Movies', $scope.movie).then(function (response) {
                blockUI.stop();
                setTimeout(function () {
                    window.location.assign('/Movie');
                    notifyService.success('Movie created with success.');
                }, 1500);
            }, function (r) {
                lib.handleError(r, form);
            });
        }
    };

    init();
}]);