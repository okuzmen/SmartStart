/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/16/13
 * Time: 5:18 PM
 * To change this template use File | Settings | File Templates.
 */

define(["angular"], function(angular){
   "use strict";
    return angular.module("myApp.controllers", []).
        controller("homectrl", ["$scope", "$injector", "dataService", function($scope, $injector, dataService){
            require(['js/controllers/homectrl'], function(homectrl) {
                // injector method takes an array of modules as the first argument
                // if you want your controller to be able to use components from
                // any of your other modules, make sure you include it together with 'ng'
                // Furthermore we need to pass on the $scope as it's unique to this controller
                $injector.invoke(homectrl, this, {'$scope': $scope, "DataService": dataService});

            });
        }]).
        controller("addnewholectrl", ["$scope", "$injector", "dataService", function($scope, $injector, dataService){
            require(['js/controllers/addnewholectrl'], function(addnewholectrl) {
                $injector.invoke(addnewholectrl, this, {'$scope': $scope, "DataService": dataService});
            });
        }]);
});
