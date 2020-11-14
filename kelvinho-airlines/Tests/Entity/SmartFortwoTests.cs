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
        public void should_disembark_all_crew_members_from_the_smart_fortwo_and_put_them_in_the_given_place()
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
        public void should_disembark_driver_from_the_smart_fortwo_and_put_him_in_the_given_place()
        {
            var driver = new Pilot("pilot");
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(driver, null);

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
            var smartFortwo = new SmartFortwo();

            smartFortwo.GetIn(null, passenger);

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

        [Fact]
        public void should_move_smart_fortwo()
        {
            var smartFortwo = new SmartFortwo();

            var origin = new Terminal(new HashSet<CrewMember>());
            origin.SetSmartFortwo(smartFortwo);
            origin.SmartFortwo.GetIn(new Pilot("pilot"), null);
            var destiny = new Airplane();

            smartFortwo.Move(origin, destiny);

            Assert.Null(origin.SmartFortwo);
            Assert.Equal(smartFortwo, destiny.SmartFortwo);
        }

        [Fact]
        public void should_return_exception_when_try_to_move_from_a_null_origin_place()
        {
            var smartFortwo = new SmartFortwo();
            var destiny = new Airplane();

            var exception = Assert.Throws<Exception>(() => smartFortwo.Move(null, destiny));

            Assert.Equal("Origin and destiny should not be null", exception.Message);
        }

        [Fact]
        public void should_return_exception_when_try_to_move_to_a_null_destiny_place()
        {
            var smartFortwo = new SmartFortwo();
            var origin = new Airplane();
            origin.SetSmartFortwo(smartFortwo);

            var exception = Assert.Throws<Exception>(() => origin.SmartFortwo.Move(origin, null));

            Assert.Equal("Origin and destiny should not be null", exception.Message);
        }

        [Fact]
        public void should_return_exception_when_move_from_a_place_that_has_no_smart_fortwo()
        {
            var smartFortwo = new SmartFortwo();
            var origin = new Airplane();
            var destiny = new Terminal(new HashSet<CrewMember>());

            var exception = Assert.Throws<Exception>(() => smartFortwo.Move(origin, destiny));

            Assert.Equal("The origin place doesn't have a smart fortwo to move", exception.Message);
        }

        [Fact]
        public void should_return_exception_when_move_a_smart_fortwo_that_has_no_driver()
        {
            var smartFortwo = new SmartFortwo();
            var origin = new Airplane();
            origin.SetSmartFortwo(smartFortwo);
            var destiny = new Terminal(new HashSet<CrewMember>());

            var exception = Assert.Throws<Exception>(() => smartFortwo.Move(origin, destiny));

            Assert.Equal("Smart Fortwo can't move without a driver", exception.Message);
        }

        public static IEnumerable<object[]> IncompatibleCrewMembers()
        {
            yield return new object[]
            {
                new Pilot("Pilot"),
                new FlightAttendant("Attendant"),
                "There is some crew members that cannot be together at the place"
            };

            yield return new object[]
            {
                new FlightServiceChief("FlightServiceChief"),
                new Officer("Officer"),
                "There is some crew members that cannot be together at the place"
            };

            yield return new object[]
            {
                new Pilot("Pilot"),
                new Prisoner("Prisoner"),
                "The prisoner can't stay with the others crew members without a policeman"
            };
        }

        [Theory]
        [MemberData(nameof(IncompatibleCrewMembers))]
        public void should_return_exception_if_imcompatible_crew_members_are_alone_at_origin_place_when_smart_fortwo_moves(
            CrewMember crewMember1,
            CrewMember crewMember2,
            string excpetedExceptionMessage)
        {
            var terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember> { crewMember1, crewMember2 });
            terminal.SmartFortwo.GetIn(new Pilot("pilot"), null);
            var destiny = new Airplane();

            var returnedException = Assert.Throws<Exception>(() => terminal.SmartFortwo.Move(terminal, destiny));

            Assert.Equal(excpetedExceptionMessage, returnedException.Message);
        }

        [Theory]
        [MemberData(nameof(IncompatibleCrewMembers))]
        public void should_return_exception_if_imcompatible_crew_members_are_alone_at_smart_fortwo_when_it_moves(
            CrewMember crewMember1,
            CrewMember crewMember2,
            string excpetedExceptionMessage)
        {
            var terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember>());
            terminal.SmartFortwo.GetIn(crewMember1, crewMember2);
            var destiny = new Airplane();

            var returnedException = Assert.Throws<Exception>(() => terminal.SmartFortwo.Move(terminal, destiny));

            Assert.Equal(excpetedExceptionMessage, returnedException.Message);
        }
    }
}