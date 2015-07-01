'use strict';

var app = angular.module('DistrictsInTown');

app.controller('mapCtrl', function($scope, DistrictServ) {
	var baseLayer = L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
		maxZoom: 18,
		id: 'webtobesocial.jokblmno',
		accessToken: 'pk.eyJ1Ijoid2VidG9iZXNvY2lhbCIsImEiOiJaU3NfOVdRIn0.k0Zr0K8bPDstktQYhXY-ZA'
	});

  $scope.updateHeatMap = function(event) {
    if (event.charCode === 32) {
      DistrictServ.getFakeLocationData().then(function (d) {
        if(d.data) {
            heatmapLayer.setData(d.data);
        }});
    }
  };

	var cfg = {
		// radius should be small ONLY if scaleRadius is true (or small radius is intended)
		radius: 1.2,
		blur: .95,
		maxOpacity: .7,
		// scales the radius based on map zoom
		scaleRadius: true,
		// if set to false the heatmap uses the global maximum for colorization
		// if activated: uses the data maximum within the current map boundaries
		//   (there will always be a red spot with useLocalExtremas true)
		useLocalExtrema: true,
		// which field name in your data represents the latitude - default "lat"
		latField: 'lat',
		// which field name in your data represents the longitude - default "lng"
		lngField: 'lng',
		// which field name in your data represents the data value - default "value"
		valueField: 'count',
		gradient: {
			// enter n keys between 0 and 1 here
			// for gradient color customization
			'.1': '#3D83BA',
			'.2': '#30A4CB',
			'.3': '#25C6DC',
			'.4': '#FAFC47',
			'.5': '#E6FC46',
			'.6': '#E6FC46',
			'.7': '#D2FC45',
			'.8': '#ADFD45',
			'.9': '#69FD43',
			'1': '#24FE41'
		}
	};


	var heatmapLayer = new HeatmapOverlay(cfg);

	var map = new L.Map('map', {
		center: new L.LatLng(52.5247, 13.38885),
		zoom: 10,
		layers: [baseLayer, heatmapLayer]
	});

	// make accessible for debugging
	var layer = heatmapLayer;
});
