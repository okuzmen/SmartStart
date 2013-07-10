'use strict';

/* Controllers */

angular.module('myApp.controllers', []).
    controller('MyCtrl1', function ($scope, DataService) {
        DataService.getAllHoles().then(function (data) {
            $scope.holes = data.results;
            $scope.$apply();
            markHollesOnMap($scope.holes).fail(function (e) {
                alert(e);
            });
        })
    })
    .controller('MyCtrl2', [function () {

    } ]);

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
        var latLng = dbGeographyPointToLatLng(hole.location.Geography.WellKnownText);
        var marker = new google.maps.Marker({
            position: latLng,
            title: hole.description
        });
        marker.setMap(map);
        bindInfoWindow(marker, map, infoWindow, buildHtmlContent(hole.image, hole.description, contentTemplate));
    }
}
function dbGeographyPointToLatLng(rawString) {

    var startIndex = rawString.indexOf("(") + 1;
    var endIndex = rawString.indexOf(")") + 1;
    var coords = rawString.substring(startIndex, endIndex).split(" ");
    var lat = parseFloat(coords[0]);
    var lng = parseFloat(coords[1]);
    return new google.maps.LatLng(lat, lng);
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
 

