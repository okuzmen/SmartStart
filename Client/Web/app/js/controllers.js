'use strict';

/* Controllers */

angular.module('myApp.controllers', []).
  controller('MyCtrl1', function($scope, DataService) {
        DataService.getAllHoles().then(function(data){
            $scope.holes = data.results;
            $scope.$apply();
            }).fail(function(e){
                alert(e);
            });
  })
  .controller('MyCtrl2', [function() {

  }]);