'use strict';

breeze.config.initializeAdapterInstance( "modelLibrary", "backingStore");

/* Controllers */

angular.module('myApp.controllers', []).
  controller('MyCtrl1', [function($scope) {
        /*var dataService = new breeze.DataService({
            serviceName: "http://localhost:25792/api/Hole",
            hasServerMetadata: false

        });*/
        var manager = new breeze.EntityManager("http://localhost:25792/breeze/Hole");
        var query = new breeze.EntityQuery().from("Holes");
        manager.executeQuery(query).then(function(data){
            $scope.holes = data.results;
            $scope.apply();
            }).fail(function(e){
                alert(e);
            });


  }])
  .controller('MyCtrl2', [function() {

  }]);