/**
 * Created with JetBrains WebStorm.
 * User: doles
 * Date: 7/16/13
 * Time: 11:02 PM
 * To change this template use File | Settings | File Templates.
 */

define(['async!https://maps.googleapis.com/maps/api/js?key=AIzaSyCWwm--593hmH9TTZOSVLXYr_SNfP0RMFU&sensor=false!callback', 'lightbox'], function(){
    return ["$scope", "DataService", function($scope, DataService){
        $scope.statusMessage = "Data fetching...";
        $scope.$apply();
        DataService.getAllHoles().then(function (data) {
            $scope.holes = data.results;
            $scope.$apply();
            markHollesOnMap($scope.holes).fail(function (e) {
                alert(e);
            });
        }).fail(function (e) {
                $scope.statusMessage = e.toString();
            });

        function bindInfoWindow(marker, map, infowindow, html) {
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.setContent(html);
                infowindow.open(map, marker);
            });
        }

        function markHollesOnMap(holes) {

            var mapOptions = {
                zoom: 8,
                /*Dnipropetrovsk coordinates*/
                center: new google.maps.LatLng(48.464764, 35.046193),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }

            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            var infoWindow = new google.maps.InfoWindow({ content: '' });
            var contentTemplate = loadContentTemplate("partials/MarkerInfoTemplate.html");

            for (var index in holes) {
                var hole = holes[index];
                var latLng = new google.maps.LatLng(hole.location.latitude, hole.location.longitude);
                var marker = new google.maps.Marker({
                    position: latLng,
                    title: hole.description
                });
                marker.setMap(map);
                bindInfoWindow(marker, map, infoWindow, buildHtmlContent(hole.imagePath, hole.description, hole.previewPath, contentTemplate));
            }
        }

        function buildHtmlContent(imagePath, description, previewPath, template) {
            return template.replace(/{fullsizeImage}/g, imagePath).replace("{previewImage}", previewPath).replace("{descrText}", description);
        }

        function loadContentTemplate(filePath) {
            //TODO: add validation check
            var xhr = new XMLHttpRequest();
            xhr.open('GET', filePath, false);
            xhr.send(null);
            return xhr.responseText;
        }

    }];
});
