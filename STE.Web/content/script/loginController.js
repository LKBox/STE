(function (steapp) {

	var loginController = function ($scope, $http, $httpParamSerializerJQLike, $location, $rootScope) {

		$rootScope.rootScopeIsLogin = false;

		$scope.login = function () {

			var loginData = {
				grant_type: 'password',
				username: $scope.username,
				password: $scope.password
			};

			var req = {
				method: 'POST',
				headers: {
					'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8'
				},
				url: rootURL + '/gettoken',
				data: $httpParamSerializerJQLike(loginData)
			}

			$http(req).then(
				function (response) {					
					localStorage.setItem("apitoken", JSON.stringify(response.data.access_token));
					$.notify("Login Success", "success");
					$location.path('/list');
				},
				function (response) {					
					$.notify("Login Failed! Please try again.", "error");
				}
			)
		};

	}

	steapp.controller("loginController", loginController);


}(angular.module("steapp")))