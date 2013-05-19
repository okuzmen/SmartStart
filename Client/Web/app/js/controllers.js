'use strict';

breeze.config.initializeAdapterInstance( "modelLibrary", "backingStore", true);
breeze.NamingConvention.camelCase.setAsDefault();

/* Controllers */

angular.module('myApp.controllers', []).
  controller('MyCtrl1', function($scope) {
        var manager = new breeze.EntityManager("http://localhost:25792/breeze/Hole");
        var query = new breeze.EntityQuery().from("Holes").orderBy("id");
        manager.executeQuery(query).then(function(data){

            $scope.holes = data.results;
            $scope.$apply();
            }).fail(function(e){
                alert(e);
            });

  })
  .controller('MyCtrl2', [function() {

  }]);