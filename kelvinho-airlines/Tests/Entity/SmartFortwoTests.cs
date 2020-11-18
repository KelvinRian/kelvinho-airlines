using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tests.Entities
{
    public class SmartFortwoTests
    {
        [Fact]
        public void should_disembark_all_crew_members_from_the_smart_fortwo()
        {
            var driver = new Pilot("pilot");
            var passenger = new Officer("officer");
            var smartFortwo = new SmartFortwo();

            smartFortwo.EnterBoth(driver, passenger);

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

            smartFortwo.EnterDriver(driver);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAll());

            Assert.Equal("There is no passenger in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_driver_from_the_smart_fortwo()
        {
            var driver = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();

            smartFortwo.EnterDriver(driver);

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

            smartFortwo.EnterPassenger(passenger);

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

        [Fact]
        public void should_set_location()
        {
            var smartFortwo = new SmartFortwo();
            smartFortwo.SetLocation(new Airplane());
            Assert.Equal(nameof(Airplane), smartFortwo.Location);
        }

        [Fact]
        public void should_enter_driver()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Pilot("driver");

            smartFortwo.EnterDriver(driver);

            Assert.Equal(driver, smartFortwo.Driver);
        }

        [Fact]
        public void should_throw_exception_if_driver_argument_of_enter_driver_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.EnterDriver(null));

            Assert.Equal("Its not possible to enter a null driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_enter_passenger()
        {
            var smartFortwo = new SmartFortwo();
            var passenger = new Pilot("passenger");

            smartFortwo.EnterPassenger(passenger);

            Assert.Equal(passenger, smartFortwo.Passenger);
        }

        [Fact]
        public void should_throw_exception_if_passenger_argument_of_enter_passenger_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.EnterPassenger(null));

            Assert.Equal("Its not possible to enter a null passenger in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_enter_both_passenger_and_driver_in_smartFortwo()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Pilot("driver");
            var passenger = new Officer("passenger");

            smartFortwo.EnterBoth(driver, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
        }

        [Fact]
        public void should_throw_exception_if_driver_argument_of_enter_both_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.EnterBoth(null, new Pilot("passenger")));

            Assert.Equal("Its not possible to enter a null driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_throw_exception_if_passenger_argument_of_enter_both_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.EnterBoth(new Pilot("driver"), null));

            Assert.Equal("Its not possible to enter a null passenger in the smart fortwo", exception.Message);
        }
    }
}