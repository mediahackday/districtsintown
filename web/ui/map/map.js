'use strict';

var app = angular.module('DistrictsInTown');

app.controller('mapCtrl', function($scope, DistrictServ) {
	$scope.updateHeatMap = function(event) {
		if (event.charCode === 32 || event.charCode === 0) {
			DistrictServ.getFakeLocationData($scope.keywords).then(function (d) {
				if (d.data) {
					var heatmap = new L.TileLayer.WebGLHeatMap({size: 1000, autoresize: true});
					// dataPoints is an array of arrays: [[lat, lng, intensity]...]
					var dataPoints = [[52.5247, 13.38885, 37]];
					for (var i = 0, len = dataPoints.length; i < len; i++) {
						var point = dataPoints[i];
						heatmap.addDataPoint(point[0],point[1],point[2]);
					}
						map.addLayer(heatmap);
				}
			});
		}
	};

	var map = L.map('map').setView([52.5247, 13.38885], 10);

	var tiles = L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
		maxZoom: 18,
		id: 'webtobesocial.jokblmno',
		accessToken: 'pk.eyJ1Ijoid2VidG9iZXNvY2lhbCIsImEiOiJaU3NfOVdRIn0.k0Zr0K8bPDstktQYhXY-ZA'
	}).addTo(map);
});