'use strict';

describe('DistrictsInTown.version module', function() {
  beforeEach(module('DistrictsInTown.version'));

  describe('version service', function() {
    it('should return current version', inject(function(version) {
      expect(version).toEqual('0.1');
    }));
  });
});
