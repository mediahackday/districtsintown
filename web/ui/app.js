'use strict';

// Declare app level module which depends on views, and components
var app = angular.module('DistrictsInTown', [
  'ui.router',
]);

app.config(function($stateProvider, $urlRouterProvider) {
  //
  // For any unmatched url, redirect to /state1
  $urlRouterProvider.otherwise("/");
  //
  // Now set up the states
  $stateProvider
    .state('map', {
      url: "/",
      templateUrl: 'map/map.html',
      controller: 'mapCtrl'
  });
});
