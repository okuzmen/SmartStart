'use strict';

// Declare app level module which depends on filters, and services
var myApp = angular.module('myApp', ['myApp.filters', 'myApp.services', 'myApp.directives', 'myApp.controllers']).
    config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/Home', {templateUrl: 'partials/partial1.html', controller: 'MyCtrl1'});
        $routeProvider.when('/AddNewHole', {templateUrl: 'partials/partial2.html', controller: 'MyCtrl2'});
        $routeProvider.otherwise({redirectTo: '/Home'});
    }]);

myApp.run(function ($rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (ev, data) {
        if (data.$$route && data.$$route.controller) {
            var index;
            var controllerName = data.$$route.controller.toString();
            if (controllerName == "MyCtrl1") {
                index = 0;
            }
            else if (controllerName == "MyCtrl2") {
                index = 1;
            }
            else {
                index = 0;
            }

            $rootScope.activeViewIndex = index;
            console.log("controller changed: " + controllerName);
        }
    })
});