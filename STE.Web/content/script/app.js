var rootURL = "http://localhost:19505";

(function () {

	var steapp = angular.module("steapp", ['ngRoute']);

	steapp.config(function ($routeProvider) {
		$routeProvider.
		when("/list", {
			templateUrl: "listpage.html",
			controller: "listController"
		}).
		when("/detail/:id", {
			templateUrl: "detailpage.html",
			controller: "detailController"
		}).
		when("/login", {
			templateUrl: "loginpage.html",
			controller: "loginController"
		}).
		otherwise({
			redirectTo: '/list'
		});
	});
	steapp.config(['$locationProvider', function ($locationProvider) {
		$locationProvider.hashPrefix('');
	}]);


	steapp.controller("logoutController", function ($scope, $location, $rootScope) {

		$rootScope.rootScopeIsLogin = false;

		$scope.logout = function () {
			if (localStorage.getItem("apitoken") != null) {
				localStorage.removeItem("apitoken");
				$location.path('/login');
			}
			else {
				$location.path('/login');
			}
		};

	});

}())






