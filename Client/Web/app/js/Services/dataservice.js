/**
 * Created with JetBrains WebStorm.
 * User: DM
 * Date: 21.05.13
 * Time: 21:31
 * To change this template use File | Settings | File Templates.
 */

define(["js/app", "breeze"], function(myApp, breeze){
    breeze.config.initializeAdapterInstance( "modelLibrary", "backingStore", true);
    breeze.NamingConvention.camelCase.setAsDefault();


    myApp.factory("dataService", function(){
        var model = "http://localhost:25792/breeze/Hole";
        var manager = new breeze.EntityManager(model);
        var metadataStore = manager.metadataStore;
        metadataStore.fetchMetadata(model);

        var dataService = {
            getAllHoles: getAllHoles,
            createHole: createHole,
            saveChanges: saveChanges
        }

        return dataService;

        function getAllHoles() {
            var query = new breeze.EntityQuery().from("GetHoles").orderBy("id");
            return manager.executeQuery(query);
        }

        function createHole(){
            return manager.createEntity('Hole');
        }

        function saveChanges(){
            manager.saveChanges();
        }
    });
});



