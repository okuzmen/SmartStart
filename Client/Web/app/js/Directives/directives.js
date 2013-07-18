/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/16/13
 * Time: 6:44 PM
 * To change this template use File | Settings | File Templates.
 */

define(["angular"], function(angular){
    "use strict";
    return angular.module("myApp.directives", []).
        directive('appVersion', ['version', function (version) {
            return function (scope, elm, attrs) {
                elm.text(version);
            };
        }]).
        directive("fileread", [function () {
            return {
                scope: {
                    fileread: "="
                },
                link: function (scope, element, attributes) {
                    element.bind("change", function (changeEvent) {
                        var reader = new FileReader();
                        reader.onload = function (loadEvent) {
                            scope.$apply(function () {
                                scope.fileread = loadEvent.target.result;
                            });
                        }
                        reader.readAsDataURL(changeEvent.target.files[0]);
                    });
                }
            }
        }]);
});