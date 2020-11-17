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
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(driver, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
        }

        [Fact]
        public void should_keep_the_driver_in_the_smart_fortwo_if_the_new_driver_argument_is_null()
        {
            var passenger = new Officer("officer");
            var driver = new Pilot("pilot");

            var smartFortwo = new SmartFortwo();
            smartFortwo.GetIn(driver, null);

            smartFortwo.GetIn(null, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
        }

        [Fact]
        public void should_keep_the_passenger_in_the_smart_fortwo_if_the_new_passenger_argument_is_null()
        {
            var driver = new Pilot("pilot");
            var passenger = new Officer("officer");
            var smartFortwo = new SmartFortwo();
            smartFortwo.GetIn(null, passenger);

            smartFortwo.GetIn(driver, null);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
        }

        [Fact]
        public void should_disembark_all_crew_members_from_the_smart_fortwo()
        {
            var driver = new Pilot("pilot");
            var passenger = new Officer("officer");
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(driver, passenger);

            var crewMembersReturned = smartFortwo.DisembarkAll();

            Assert.Null(smartFortwo.Driver);
            Assert.Null(smartFortwo.Passenger);
            Assert.Equal(2, crewMembersReturned.Count());
            Assert.Contains(driver, crewMembersReturned);
            Assert.Contains(passenger, crewMembersReturned);
        }

        [Fact]
        public void should_return_exception_if_driver_is_null_when_try_to_disembark_all()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAll());

            Assert.Equal("There is no driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_passenger_is_null_when_try_to_disembark_all()
        {
            var driver = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(driver, null);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAll());

            Assert.Equal("There is no passenger in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_driver_from_the_smart_fortwo()
        {
            var driver = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(driver, null);

            var driverReturned = smartFortwo.DisembarkDriver();

            Assert.Null(smartFortwo.Driver);
            Assert.Equal(driver, driverReturned);
        }


        [Fact]
        public void should_return_exception_when_try_to_disembark_a_driver_that_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkDriver());

            Assert.Equal("There is no driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_passenger_from_the_smart_fortwo()
        {
            var passenger = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(null, passenger);

            var destinyPlace = new Airplane();
            destinyPlace.SetSmartFortwo(smartFortwo);

            var returnedPassenger = smartFortwo.DisembarkPassenger();

            Assert.Null(smartFortwo.Driver);
            Assert.Equal(passenger, returnedPassenger);
        }

        [Fact]
        public void should_return_exception_when_try_to_disembark_a_passenger_that_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkPassenger());

            Assert.Equal("There is no passenger in the smart fortwo", exception.Message);
        }
    }
}