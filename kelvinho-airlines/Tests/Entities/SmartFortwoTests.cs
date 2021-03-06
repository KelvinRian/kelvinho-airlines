﻿using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using System;
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

        [Fact]
        public void to_string_method_should_show_return_smart_fortwo_information()
        {
            var smartFortwo = new SmartFortwo();
            smartFortwo.EnterBoth(new Pilot("driverName"), new Officer("passengerName"));

            var result = smartFortwo.ToString();

            Assert.Equal("Smart Fortwo:   |   Driver: Pilot driverName   |   Passenger: Officer passengerName", result);
        }

        [Fact]
        public void to_string_method_should_return_Empty_for_names_and_an_empty_string_for_type_when_crewMembers_are_null()
        {
            var smartFortwo = new SmartFortwo();

            var result = smartFortwo.ToString();

            Assert.Equal("Smart Fortwo:   |   Driver:  Empty   |   Passenger:  Empty", result);
        }

        [Fact]
        public void should_get_crew_members()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Pilot("pilot name");
            var passenger = new Officer("officer name");

            smartFortwo.EnterBoth(driver, passenger);

            var returnedCrewMembers = smartFortwo.GetCrewMembers();

            Assert.Equal(2, returnedCrewMembers.Count());
            Assert.Contains(driver, returnedCrewMembers);
            Assert.Contains(passenger, returnedCrewMembers);

            Assert.NotNull(smartFortwo.Driver);
            Assert.NotNull(smartFortwo.Passenger);
        }
    }
}