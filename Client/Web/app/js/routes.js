/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/17/13
 * Time: 2:31 PM
 * To change this template use File | Settings | File Templates.
 */

define(['angular', 'js/app'], function(angular, app) {

    'use strict';
    var homeControllerName = "homectrl";
    var addNewHoleControllerName = "addnewholectrl";

    app.run(["$rootScope", function ($rootScope) {
        $rootScope.serverAddress = "http://localhost:25792/";
        $rootScope.$on('$routeChangeSuccess', function (ev, data) {
            if (data.$$route && data.$$route.controller) {
                var index;
                var controllerName = data.$$route.controller.toString();
                if (controllerName == homeControllerName) {
                    index = 0;
                }
                else if (controllerName == addNewHoleControllerName) {
                    index = 1;
                }
                else {
                    index = 0;
                }

                $rootScope.activeViewIndex = index;
                console.log("controller changed: " + controllerName);
            }
        })
    }]);

    return app.config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/Home', {
            templateUrl: 'partials/partial1.html',
            controller: homeControllerName
        });
        $routeProvider.when('/AddNewHole', {
            templateUrl: 'partials/partial2.html',
            controller: addNewHoleControllerName
        });
        $routeProvider.otherwise({redirectTo: '/Home'});

    }]);

});
