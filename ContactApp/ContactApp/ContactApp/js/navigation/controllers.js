/* global angular */
(function () {
    "use strict";
    var app = angular.module('navigation.module', []);

    app.controller('navigationCtrl', ['$scope', '$location',
        function ($scope, $location) {
            $scope.IsRouteActive = function (routePath) {
                return routePath === $location.path();
            };
        }
    ]);
})();
