/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/15/13
 * Time: 4:35 PM
 * To change this template use File | Settings | File Templates.
 */

require.config({
        paths : {
            angular : "lib/angular/angular",
            jquery : "lib/jquery/jquery-1.8.3",
            breeze : "lib/breeze/breeze.debug",
            domReady: "lib/domReady/domReady",
            Q : "lib/qjs/q"
        },
        shim: {
            angular : {
                deps : ["jquery"],
                exports : "angular"
            },
            breeze : {
                deps : ["jquery", "Q"],
                exports : "breeze"
            }
        },
        priority: [
            "angular", "breeze", "google"
        ]
    }
);

require({
    waitSeconds : 120, //make sure it is enough to load all gmaps scripts
    paths : {
        async : 'lib/requirejs/async' //alias to plugin
    }
});

require([
    "angular",
    "js/app",
    "js/routes",
    "js/services/dataservice"
], function(angular, app){
        angular.bootstrap(document, [app['name']]);
    }
)
