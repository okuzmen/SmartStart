define([
    "angular",
    "angularResources",
    "js/filters/filters",
    "js/controllers/controllers",
    "js/directives/directives"],
    function(angular, filters, controllers, directives){
        'use strict';
        return angular.module("myApp", ['ngResource', 'myApp.filters', 'myApp.directives', 'myApp.controllers']);
    }
);