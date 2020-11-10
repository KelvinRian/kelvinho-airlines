using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Entities
{
    public class SmartFortwoTests
    {
        [Fact]
        public void should_get_in_the_smart_fortwo()
        {
            var driver = new Pilot("pilot");
            var passenger = new Officer("officer");
            var originPlace = new Terminal(new HashSet<CrewMember> { driver, passenger });
            var smartFortwo = new SmartFortwo();

            var crewMembersReturned = smartFortwo.GetIn(originPlace, driver, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);

            var expectedQuantity = 2;
            Assert.Equal(expectedQuantity, crewMembersReturned.Count());
            Assert.Contains(driver, crewMembersReturned);
            Assert.Contains(passenger, crewMembersReturned);

            Assert.Empty(originPlace.CrewMembers);
        }

        [Fact]
        public void should_keep_the_driver_in_the_smart_fortwo_if_the_new_driver_argument_is_null()
        {
            var passenger = new Officer("officer");
            var originPlace = new Terminal(new HashSet<CrewMember> { passenger });

            var driver = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();
            smartFortwo.GetIn(originPlace, driver, null);

            var crewMembersReturned = smartFortwo.GetIn(originPlace, null, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);

            var expectedQuantity = 2;
            Assert.Equal(expectedQuantity, crewMembersReturned.Count());
            Assert.Contains(passenger, crewMembersReturned);
            Assert.DoesNotContain(driver, crewMembersReturned);

            Assert.Empty(originPlace.CrewMembers);
        }

        [Fact]
        public void should_keep_the_passenger_in_the_smart_fortwo_if_the_new_passenger_argument_is_null()
        {
            var driver = new Pilot("pilot");
            var originPlace = new Terminal(new HashSet<CrewMember> { driver });

            var passenger = new Officer("officer");
            var smartFortwo = new SmartFortwo();
            smartFortwo.GetIn(originPlace, null, passenger);

            var crewMembersReturned = smartFortwo.GetIn(originPlace, driver, null);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);

            var expectedQuantity = 2;
            Assert.Equal(expectedQuantity, crewMembersReturned.Count());
            Assert.Contains(driver, crewMembersReturned);
            Assert.DoesNotContain(passenger, crewMembersReturned);

            Assert.Empty(originPlace.CrewMembers);
        }

        [Fact]
        public void should_return_exception_when_place_argument_of_get_in_method_is_null()
        {
            var smartFortwo = new SmartFortwo();
            var exception = Assert.Throws<Exception>(() => smartFortwo.GetIn(null, new Pilot("pilot"), new Officer("officer")));
            Assert.Equal("Place should not be null", exception.Message);
        }
    }
}