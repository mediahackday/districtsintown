'use strict';

angular.module('DistrictsInTown.map', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/map', {
    templateUrl: 'map/map.html',
    controller: 'mapCtrl'
  });
}])

.controller('mapCtrl', [function() {
	var map = L.map('map').setView([51.505, -0.09], 13);
	L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
		maxZoom: 18,
		id: 'webtobesocial.jokblmno',
		accessToken: 'pk.eyJ1Ijoid2VidG9iZXNvY2lhbCIsImEiOiJaU3NfOVdRIn0.k0Zr0K8bPDstktQYhXY-ZA'
	}).addTo(map);

	map.locate({setView: true, maxZoom: 16});

	function onLocationFound(e) {
		var radius = e.accuracy / 2;

		L.marker(e.latlng).addTo(map)
			.bindPopup("You are within " + radius + " meters from this point").openPopup();

		L.circle(e.latlng, radius).addTo(map);
	}

	map.on('locationfound', onLocationFound);

	function onLocationError(e) {
		alert(e.message);
	}

	map.on('locationerror', onLocationError);
}]);
