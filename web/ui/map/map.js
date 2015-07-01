'use strict';

var app = angular.module('DistrictsInTown');

app.controller('mapCtrl', function($scope, DistrictServ) {
	var heatmap = null;

	$scope.updateHeatMap = function(event) {
		if (event.charCode === 32 || event.charCode === 0) {
			DistrictServ.getLocationData($scope.keywords).then(function (d) {
				if (map.hasLayer(heatmap)) {
					map.removeLayer(heatmap);
				}

				if (d.data.data) {
					heatmap = new L.TileLayer.WebGLHeatMap({size: 1000, autoresize: true});
					for (var i = 0, len = d.data.data.length; i < len; i++) {
						var point = d.data.data[i];
						heatmap.addDataPoint(point.lat,point.lng,point.count*8);
					}
					map.addLayer(heatmap);
				}
			});
		}
	};

	var map = L.map('map').setView([52.5247, 13.38885], 12);

	var tiles = L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
		maxZoom: 18,
		id: 'webtobesocial.jokblmno',
		accessToken: 'pk.eyJ1Ijoid2VidG9iZXNvY2lhbCIsImEiOiJaU3NfOVdRIn0.k0Zr0K8bPDstktQYhXY-ZA'
	}).addTo(map);
});