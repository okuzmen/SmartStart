define([
    "angular",
    "js/filters/filters",
    "js/controllers/controllers",
    "js/directives/directives"],
    function(angular, filters, controllers, directives){
        'use strict';
        return angular.module("myApp", ['myApp.filters', 'myApp.directives', 'myApp.controllers']);
    }
);