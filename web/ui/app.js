'use strict';

// Declare app level module which depends on views, and components
angular.module('DistrictsInTown', [
  'ngRoute',
  'DistrictsInTown.map',
  'DistrictsInTown.version'
]).
config(['$routeProvider', function($routeProvider) {
  $routeProvider.otherwise({redirectTo: '/map'});
}]);
