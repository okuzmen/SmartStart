'use strict';

/* Controllers */

angular.module('myApp.controllers', []).
    controller('MyCtrl1', function ($scope, DataService) {
        $scope.statusMessage = "Data fetching...";
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
                bindInfoWindow(marker, map, infoWindow, buildHtmlContent(hole.imagePath, hole.description, contentTemplate));
            }
        }

        function buildHtmlContent(imagePath, description, template) {
            return template.replace("{imgPath}", imagePath).replace("{descrText}", description);
        }

        function loadContentTemplate(filePath) {
            //TODO: add validation check
            var xhr = new XMLHttpRequest();
            xhr.open('GET', filePath, false);
            xhr.send(null);
            return xhr.responseText;
        }
    })

    .controller('MyCtrl2', function ($scope, DataService) {

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
            newHole.location.latitude = hole.location.latitude;
            newHole.location.longitude = hole.location.longitude;
            newHole.description = hole.description;
            newHole.status = 0; //New
            newHole.imagePath = "path to image";
            DataService.saveChanges();
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
    });

 

