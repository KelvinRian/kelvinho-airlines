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

            smartFortwo.GetIn(originPlace, driver, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
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

            smartFortwo.GetIn(originPlace, null, passenger);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
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

            smartFortwo.GetIn(originPlace, driver, null);

            Assert.Equal(driver, smartFortwo.Driver);
            Assert.Equal(passenger, smartFortwo.Passenger);
            Assert.Empty(originPlace.CrewMembers);
        }

        [Fact]
        public void should_return_exception_if_place_argument_of_get_in_method_is_null()
        {
            var smartFortwo = new SmartFortwo();
            var exception = Assert.Throws<Exception>(() => smartFortwo.GetIn(null, new Pilot("pilot"), new Officer("officer")));
            Assert.Equal("Place should not be null", exception.Message);
        }

        [Fact]
        public void should_disembark_all_crew_members_from_the_smart_fortwo_and_put_them_in_the_given_place()
        {
            var driver = new Pilot("pilot");
            var passenger = new Officer("officer");
            var originPlace = new Terminal(new HashSet<CrewMember> { driver, passenger });
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(originPlace, driver, passenger);

            var destinyPlace = new Airplane();
            destinyPlace.SetSmartFortwo(smartFortwo);
            var crewMembersReturned = smartFortwo.DisembarkAllIn(destinyPlace);

            Assert.Null(smartFortwo.Driver);
            Assert.Null(smartFortwo.Passenger);

            Assert.Contains(driver, destinyPlace.CrewMembers);
            Assert.Contains(passenger, destinyPlace.CrewMembers);

            Assert.Equal(2, crewMembersReturned.Count());
            Assert.Contains(driver, crewMembersReturned);
            Assert.Contains(passenger, crewMembersReturned);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_all_in_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAllIn(null));

            Assert.Equal("Place should not be null", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_all_in_method_has_no_smart_fortwo()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAllIn(airplane));

            Assert.Equal("The smart fortwo isn't at the place", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_driver_is_null_when_try_to_disembark_all()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();
            airplane.SetSmartFortwo(smartFortwo);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAllIn(airplane));

            Assert.Equal("There is no driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_passenger_is_null_when_try_to_disembark_all()
        {
            var driver = new Pilot("pilot");
            var originPlace = new Terminal(new HashSet<CrewMember> { driver });
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(originPlace, driver, null);

            var destinyPlace = new Airplane();
            destinyPlace.SetSmartFortwo(smartFortwo);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkAllIn(destinyPlace));

            Assert.Equal("There is no passenger in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_driver_from_the_smart_fortwo_and_put_him_in_the_given_place()
        {
            var driver = new Pilot("pilot");
            var originPlace = new Terminal(new HashSet<CrewMember> { driver });
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(originPlace, driver, null);

            var destinyPlace = new Airplane();
            destinyPlace.SetSmartFortwo(smartFortwo);

            var driverReturned = smartFortwo.DisembarkDriverIn(destinyPlace);

            Assert.Null(smartFortwo.Driver);
            Assert.Contains(driver, destinyPlace.CrewMembers);
            Assert.Equal(driver, driverReturned);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_driver_in_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkDriverIn(null));

            Assert.Equal("Place should not be null", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_driver_in_method_has_no_smart_fortwo()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkDriverIn(airplane));

            Assert.Equal("The smart fortwo isn't at the place", exception.Message);
        }

        [Fact]
        public void should_return_exception_when_try_to_disembark_a_driver_that_is_null()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();
            airplane.SetSmartFortwo(smartFortwo);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkDriverIn(airplane));

            Assert.Equal("There is no driver in the smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_passenger_from_the_smart_fortwo_and_put_him_in_the_given_place()
        {
            var passenger = new Pilot("pilot");
            var originPlace = new Terminal(new HashSet<CrewMember> { passenger });
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(originPlace, null, passenger);

            var destinyPlace = new Airplane();
            destinyPlace.SetSmartFortwo(smartFortwo);

            var returnedPassenger = smartFortwo.DisembarkPassengerIn(destinyPlace);

            Assert.Null(smartFortwo.Driver);
            Assert.Contains(passenger, destinyPlace.CrewMembers);
            Assert.Equal(passenger, returnedPassenger);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_passenger_in_method_is_null()
        {
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkPassengerIn(null));

            Assert.Equal("Place should not be null", exception.Message);
        }

        [Fact]
        public void should_return_exception_if_the_place_argument_of_disembark_passenger_in_method_has_no_smart_fortwo()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkPassengerIn(airplane));

            Assert.Equal("The smart fortwo isn't at the place", exception.Message);
        }

        [Fact]
        public void should_return_exception_when_try_to_disembark_a_passenger_that_is_null()
        {
            var airplane = new Airplane();
            var smartFortwo = new SmartFortwo();
            airplane.SetSmartFortwo(smartFortwo);

            var exception = Assert.Throws<Exception>(() => smartFortwo.DisembarkPassengerIn(airplane));

            Assert.Equal("There is no passenger in the smart fortwo", exception.Message);
        }
    }
}