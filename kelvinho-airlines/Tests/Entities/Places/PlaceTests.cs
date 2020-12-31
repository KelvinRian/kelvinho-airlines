using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks;
using Xunit;

namespace Tests.Entities.Places
{
    public class PlaceTests
    {
        [Fact]
        public void should_get_crew_members_from_smart_fortwo()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new FlightServiceChief("driver name");
            var passenger = new Officer("passenger name");
            smartFortwo.EnterBoth(driver, passenger);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var crewMembersReturned = place.GetSmartFortwoCrewMembers();

            Assert.Equal(2, crewMembersReturned.Count());
            Assert.Contains(driver, crewMembersReturned);
            Assert.Contains(passenger, crewMembersReturned);
        }

        [Fact]
        public void should_throw_exception_when_try_to_get_crew_member_from_a_null_smart_fortwo()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.GetSmartFortwoCrewMembers());
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_check_whether_smart_fortwo_has_driver_and_return_true_if_it_has()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Pilot("driver name");
            smartFortwo.EnterDriver(driver);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var hasDriver = place.SmartFortwoHasDriver();

            Assert.True(hasDriver);
        }

        [Fact]
        public void should_check_whether_smart_fortwo_has_driver_and_return_false_if_it_doesnt_have()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            var hasDriver = place.SmartFortwoHasDriver();

            Assert.False(hasDriver);
        }

        [Fact]
        public void should_check_whether_smart_fortwo_has_driver_and_throw_exception_if_it_is_null()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.SmartFortwoHasDriver());
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_passenger_from_smart_fortwo_and_return_it()
        {
            var smartFortwo = new SmartFortwo();
            var passenger = new Officer("passenger name");
            smartFortwo.EnterPassenger(passenger);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var returnedCrewMember = place.DisembarkPassengerFromSmartFortwo();

            Assert.Null(place.SmartFortwo.Passenger);
            Assert.Equal(passenger, returnedCrewMember);
        }

        [Fact]
        public void should_throws_exception_when_try_to_disembark_passenger_if_smart_fortwo_is_null()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.DisembarkPassengerFromSmartFortwo());
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_driver_from_smart_fortwo_and_return_it()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Officer("driver name");
            smartFortwo.EnterDriver(driver);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var returnedCrewMember = place.DisembarkDriverFromSmartFortwo();

            Assert.Null(place.SmartFortwo.Driver);
            Assert.Equal(driver, returnedCrewMember);
        }

        [Fact]
        public void should_throws_exception_when_try_to_disembark_driver_if_smart_fortwo_is_null()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.DisembarkDriverFromSmartFortwo());
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_disembark_all_from_smart_fortwo_and_return_it()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Pilot("driver name");
            var passenger = new Policeman("passenger name");
            smartFortwo.EnterBoth(driver, passenger);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var returnedCrewMembers = place.DisembarkAllFromSmartFortwo();

            Assert.Null(place.SmartFortwo.Driver);
            Assert.Null(place.SmartFortwo.Passenger);
            Assert.Equal(2, returnedCrewMembers.Count());
            Assert.Contains(driver, returnedCrewMembers);
            Assert.Contains(passenger, returnedCrewMembers);
        }

        [Fact]
        public void should_throws_exception_when_try_to_disembark_all_if_smart_fortwo_is_null()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.DisembarkAllFromSmartFortwo());
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_set_crew_members_and_null_smart_fortwo_exception_in_constructor()
        {
            var place = new PlaceMock();

            Assert.Empty(place.CrewMembers);
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", place.GetNullSmartFortwoException());
        }

        [Fact]
        public void should_remove_smart_fortwo()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            place.RemoveSmartFortwo();
            
            Assert.Null(place.SmartFortwo);
        }

        [Fact]
        public void should_board_a_single_crew_member()
        {
            var place = new PlaceMock();
            var crewMember = new Policeman("crew member name");

            place.Board(crewMember);

            Assert.Contains(crewMember, place.CrewMembers);
        }

        [Fact]
        public void should_board_a_list_of_crew_members()
        {
            var place = new PlaceMock();
            var crewMembers = new List<CrewMember>
            {
                new Policeman("policeman name"),
                new Prisoner("prisoner name")
            };

            place.Board(crewMembers);

            Assert.True(place.CrewMembers.All(x => crewMembers.Contains(x)));
            Assert.Equal(crewMembers.Count(), place.CrewMembers.Count());
        }

        [Fact]
        public void should_remove_crew_members()
        {
            var place = new PlaceMock();
            var crewMemberThatMustStay = new Pilot("pilot name");
            var crewMembersThatMustBeRemoved = new List<CrewMember>
            {
                new Policeman("policeman name"),
                new Prisoner("prisoner name"),
            };
            
            var crewMembers = new List<CrewMember>();
            crewMembers.Add(crewMemberThatMustStay);
            crewMembers.AddRange(crewMembersThatMustBeRemoved);

            place.Board(crewMembers);

            place.Remove(crewMembersThatMustBeRemoved.First(), crewMembersThatMustBeRemoved.Last());

            Assert.Contains(crewMemberThatMustStay, place.CrewMembers);
            Assert.Single(place.CrewMembers);
        }

        [Fact]
        public void should_put_driver_in_smart_fortwo()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            var driver = new Pilot("pilot name");

            place.PutDriverInSmartFortwo(driver);

            Assert.Equal(driver, place.SmartFortwo.Driver);
        }

        [Fact]
        public void should_throws_exception_when_try_to_put_driver_in_a_null_smart_fortwo()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.PutDriverInSmartFortwo(new Officer("name")));
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_put_passenger_in_smart_fortwo()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            var passenger = new Pilot("passenger name");

            place.PutPassengerInSmartFortwo(passenger);

            Assert.Equal(passenger, place.SmartFortwo.Passenger);
        }

        [Fact]
        public void should_throws_exception_when_try_to_put_passenger_in_a_null_smart_fortwo()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.PutPassengerInSmartFortwo(new Officer("name")));
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }

        [Fact]
        public void should_put_both_in_smart_fortwo()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            var passenger = new Prisoner("passenger name");
            var driver = new Policeman("pilot name");
            place.PutBothInSmartFortwo(driver, passenger);

            Assert.Equal(passenger, place.SmartFortwo.Passenger);
            Assert.Equal(driver, place.SmartFortwo.Driver);
        }

        [Fact]
        public void should_throws_exception_when_try_to_put_both_in_a_null_smart_fortwo()
        {
            var place = new PlaceMock();
            var exception = Assert.Throws<Exception>(() => place.PutBothInSmartFortwo(new Officer("name"), new Pilot("name")));
            Assert.Equal($"{place.GetType().Name} does not have a smart fortwo", exception.Message);
        }
    }
}
