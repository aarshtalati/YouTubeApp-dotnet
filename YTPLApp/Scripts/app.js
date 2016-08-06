angular.module('app', [])

	.service('youTubeService', ['$http', function ($http) {
	    this.get = function (resourceUrl) {
	        return $http.get(resourceUrl);
	    }
	}])

    .controller('youTubeController', ['$scope', 'youTubeService', function ($scope, service) {
        var rootApiUrl = 'api/YouTube/'
        $scope.results = []

        $scope.youtubeQuery = '';
        $scope.youtubeType = '';

        $scope.submit = function () {
            if (($scope.youtubeQuery === undefined || $scope.youtubeQuery === '') && ($scope.youtubeType === undefined || $scope.youtubeType === '')) {
                $scope.action_url = rootApiUrl;
            }
            service.get($scope.action_url).success(function (data, status) {
                $scope.results = data;
            }).error(function (data, status) {
                alert('Error, see browser console for more details');
                console.log(data);
            });
        };

        $scope.$watch('youtubeType', function () {
            $scope.action_url = rootApiUrl + ($scope.youtubeType == "playlist" ? "GetVideoByPlaylist/" : "GetVideosByUser/").toString() + ($scope.youtubeQuery).toString();
        })
        $scope.$watch('youtubeQuery', function () {
            $scope.action_url = rootApiUrl + ($scope.youtubeType == "playlist" ? "GetVideoByPlaylist/" : "GetVideosByUser/").toString() + ($scope.youtubeQuery).toString();
        })
    }]);