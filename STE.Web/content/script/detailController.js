(function (steapp) {

	var detailController = function ($scope, $http, $location, $routeParams, $rootScope, $sce) {

		if (localStorage.getItem("apitoken") == null) {
			$location.path('/login');
		}

		$rootScope.rootScopeIsLogin = true;

		var accessToken = $.parseJSON(localStorage.getItem("apitoken"));
		var articleId = $routeParams.id;

		if (articleId == 0) {

			$scope.isNew = true;

			$scope.create = function (article) {

				var req = {
					method: 'POST',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/Articles/PostArticle',
					data: article
				}

				$http(req).then(
					function (response) {
						$.notify("Success", "success");
						$location.path('/detail/' + response.data.Id);
					},
					function (response) {
						$.notify("Failed! Please try again.", "error");
					}
				)
			}

		}
		else {

			$scope.isNew = false;

			GetArticleDetail(articleId);

			function GetArticleDetail(articleId) {
				var req = {
					method: 'GET',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/Articles/GetArticle/' + articleId
				}

				$http(req).then(
					function (response) {
						$scope.article = response.data;
						GetArticleVersions(response.data.Id);
					},
					function (response) {
						$.notify("Failed! Please try again.", "error");
					}
				)
			}

			function GetArticleVersions(articleId) {
				var req = {
					method: 'GET',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/ArticleVersions/GetArticleVersionsByArticleId?articleId=' + articleId
				}

				$http(req).then(
					function (response) {
						$scope.articleVersions = response.data;
					},
					function (response) {
						$.notify("Failed! Please try again.", "error");
					}
				)
			}

			$scope.update = function (article) {

				if (article.ArticleText != "") {
					var req = {
						method: 'PUT',
						headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
						url: rootURL + '/api/Articles/PutArticle/' + article.Id,
						data: article
					}

					$http(req).then(
						function (response) {
							$.notify("Success", "success");
							GetArticleVersions(article.Id);
						},
						function (response) {
							$.notify("Failed! Please try again.", "error");
						}
					)
				}
			}

			$scope.deleteVersion = function (vId) {
				var req = {
					method: 'DELETE',
					headers: { "accept": "application/json", "content-type": "application/json", "authorization": "Bearer " + accessToken },
					url: rootURL + '/api/ArticleVersions/DeleteArticleVersion/' + vId
				}

				$http(req).then(
					function (response) {
						for (var i = 0; i < $scope.articleVersions.length; i++) {
							if ($scope.articleVersions[i].Id == vId) {
								$scope.articleVersions.splice(i, 1);
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

			$scope.openVersion = function (version) {

				$scope.showOld = true;
				$scope.oldVersion = version;
				$("#oldVersionModal").modal("show");
			}

			$scope.closeVersion = function () {

				$scope.showOld = false;
				$scope.oldVersion = null;

			}

		}

	}

	steapp.controller("detailController", detailController);

	steapp.directive('contenteditable', function () {
		return {
			require: 'ngModel',
			link: function (scope, element, attrs, ctrl) {				
				element.bind('blur', function () {
					scope.$apply(function () {
						ctrl.$setViewValue(element.html());
					});
				});
				ctrl.$render = function () {
					element.html(ctrl.$viewValue);
				};
				ctrl.$render();
			}
		};
	});

	steapp.filter('highlight', function ($sce) {
		return function(text, phrase) {
			if (phrase) text = text.replace(new RegExp('('+phrase+')', 'gi'),
			  '<span class="highlighted">$1</span>')

			return $sce.trustAsHtml(text)
		}
	})


}(angular.module("steapp")))