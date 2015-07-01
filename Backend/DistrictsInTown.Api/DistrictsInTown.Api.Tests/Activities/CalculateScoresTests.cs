using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Activities;
using DistrictsInTown.Api.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistrictsInTown.Api.Tests.Activities
{
    [TestClass]
    public class CalculateScoresTests
    {
        [TestMethod]
        public void CalculateScoresForPlacesTest()
        {
            var places = new List<Place>
            {
                new Place {Latitude = 53.1234, Longitude = 13.1234, Score = 7, ZipCode = "123456"},
                new Place {Latitude = 53.2234, Longitude = 13.1234, Score = 8, ZipCode = "123456"},
                new Place {Latitude = 53.3234, Longitude = 13.1234, Score = 9, ZipCode = "123456"},
                new Place {Latitude = 53.4234, Longitude = 13.1234, Score = 7, ZipCode = "123456"},
                new Place {Latitude = 54.1234, Longitude = 14.1234, Score = 2, ZipCode = "223456"},
                new Place {Latitude = 54.2234, Longitude = 14.1234, Score = 3, ZipCode = "223456"},
                new Place {Latitude = 54.3234, Longitude = 14.1234, Score = 4, ZipCode = "223456"},
                new Place {Latitude = 54.4234, Longitude = 14.1234, Score = 5, ZipCode = "223456"}
            };

            var calculateScores = new CalculateScores();
            var results = calculateScores.For(places).ToList();

            results.Should().NotBeNull();
            results.Any().Should().BeTrue();
            results.Count.Should().Be(2);
            results[0].Score.Should().Be(7.75);
            results[1].Score.Should().Be(3.5);
        }
    }
}