(function (steapp) {

	var listController = function ($scope, $http, $location, $rootScope) {

		if (localStorage.getItem("apitoken") == null) {
			$location.path('/login');
		}
		else {
			$rootScope.rootScopeIsLogin = true;

			var accessToken = $.parseJSON(localStorage.getItem("apitoken"));

			GetAllArticles();

			function GetAllArticles() {

				var req = {
					method: 'GET',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/Articles/GetArticles'
				}

				$http(req).then(
					function (response) {
						$scope.articles = response.data;
					},
					function (response) {
						$.notify("Failed! Please try again.", "error");
					}
				)
			}

			$scope.delete = function (article) {

				var req = {
					method: 'DELETE',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/Articles/DeleteArticle/' + article.Id
				}

				$http(req).then(
					function (response) {

						for (var i = 0; i < $scope.articles.length; i++) {
							if ($scope.articles[i].Id == article.Id) {
								$scope.articles.splice(i, 1);
								break;
							}
						}
						$.notify("Deleted", "success");
					},
					function (response) {
						$.notify("Failed! Please try again.", "error");
					}
				)
			}
		}		

	}

	steapp.controller("listController", listController);


}(angular.module("steapp")))