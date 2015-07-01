using System.Linq;
using DistrictsInTown.Api.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistrictsInTown.Api.Tests.Repositories
{
    [TestClass]
    public class PlaceRepositoryTests
    {
        [TestMethod]
        public void ReadPlacesByKeywordTest()
        {
            var repositoryUnderTest = new PlaceRepository();
            var places = repositoryUnderTest.Get("Bikes").ToList();

            places.Should().NotBeNull();
            places.Any().Should().BeTrue();
            places.Count.Should().Be(1);
        }

        [TestMethod]
        public void ReadAllPlacesTest()
        {
            var repositoryUnderTest = new PlaceRepository();
            var places = repositoryUnderTest.Get(string.Empty).ToList();

            places.Should().NotBeNull();
            places.Any().Should().BeTrue();
        }
    }
}