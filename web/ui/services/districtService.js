'use strict';

var app = angular.module('DistrictsInTown');

app.factory('DistrictServ', function($http) {

  return {
            getFakeLocationData: function () {
                var res =  $http({
                    url: "http://localhost:9000/fake.json",
                    method: "GET"
                 });

                return res;
            },

            getLocationData: function (keywords) {
                keywords = keywords.split(" ");
                var res =  $http({
                    url: 'api.de/location?keywords='+keywords.toString(),
                    method: "GET"
                 });

                return res;
              }
            };
  });
