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
                var url = 'http://districtsintown.azurewebsites.net/api/place?';
                for (var i = 0; i < keywords.length; i++) {
                  url = url + 'keyword=' + keywords[i] + '&';
                }
                var res =  $http({
                    url: url,
                    method: "GET"
                 });

                return res;
              }
            };
  });
