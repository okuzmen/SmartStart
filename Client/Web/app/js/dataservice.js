/**
 * Created with JetBrains WebStorm.
 * User: DM
 * Date: 21.05.13
 * Time: 21:31
 * To change this template use File | Settings | File Templates.
 */
breeze.config.initializeAdapterInstance( "modelLibrary", "backingStore", true);
breeze.NamingConvention.camelCase.setAsDefault();

myApp.factory("DataService", function(){
    var manager = new breeze.EntityManager("http://localhost:25792/breeze/Hole");

    var dataService = {
        getAllHoles: getAllHoles
    }

    return dataService;

    function getAllHoles() {
        var query = new breeze.EntityQuery().from("GetHoles").orderBy("id");
        return manager.executeQuery(query);
    }
});
