'use strict';

angular.module('DistrictsInTown.version', [
  'DistrictsInTown.version.interpolate-filter',
  'DistrictsInTown.version.version-directive'
])

.value('version', '0.1');
