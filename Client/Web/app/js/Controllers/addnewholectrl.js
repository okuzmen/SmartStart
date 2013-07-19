/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/17/13
 * Time: 9:50 PM
 * To change this template use File | Settings | File Templates.
 */

define(['async!https://maps.googleapis.com/maps/api/js?key=AIzaSyCWwm--593hmH9TTZOSVLXYr_SNfP0RMFU&sensor=false!callback'], function () {
    return ["$scope", "DataService", function ($scope, DataService) {
        //Init hole model.
        $scope.hole = {
            description: "",
            location: ""
        };

        var mapOptions = {
            zoom: 8,
            /*Dnipropetrovsk coordinates*/
            center: new google.maps.LatLng(48.464764, 35.046193),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

        google.maps.event.addListener(map, "rightclick", function (event) {
            event.cancelBubble = true;
            var lat = event.latLng.lat();
            var lng = event.latLng.lng();
            $scope.hole.location = {
                latitude: lat,
                longitude: lng
            };

            setNewMarker(lat, lng);

        });

        $scope.createHole = function () {
            var hole = $scope.hole; //new hole
            var newHole = DataService.createHole();
            $scope.result = DataService.addImage(hole.image, function (result) {
                newHole.location.latitude = hole.location.latitude;
                newHole.location.longitude = hole.location.longitude;
                newHole.description = hole.description;
                newHole.status = 0; //New
                newHole.imagePath = result.fullSizeImage;
                newHole.previewPath = result.previewSizeImage;
                console.log(newHole);
                DataService.saveChanges();
            });
        };

        $scope.setFile = function (element) {
            alert(element);
        };

        function setNewMarker(lat, lng) {
            var latLng = new google.maps.LatLng(lat, lng);
            var marker = new google.maps.Marker({
                position: latLng,
                title: ""
            });
            if ($scope.previousMarker != undefined) {
                $scope.previousMarker.setMap(null);

            }
            $scope.previousMarker = marker;
            marker.setMap(map);
        }

    }];
});

