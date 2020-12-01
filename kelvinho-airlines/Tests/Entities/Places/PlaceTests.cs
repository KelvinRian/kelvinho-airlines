using kelvinho_airlines.Entities;
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
        public void should_check_whether_smart_fortwo_has_driver_and_return_true_when_it_has()
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
        public void should_check_whether_smart_fortwo_has_driver_and_return_true_when_it_doesnt_have()
        {
            var place = new PlaceMock();
            place.SetSmartFortwo(new SmartFortwo());

            var hasDriver = place.SmartFortwoHasDriver();

            Assert.False(hasDriver);
        }

        [Fact]
        public void should_disembark_passenger_from_smart_fortwo_and_return_it()
        {
            var smartFortwo = new SmartFortwo();
            var passenger = new Officer("passenger name");
            smartFortwo.EnterPassenger(passenger);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var returnedCrewMember = place.DisembarkSmartFortwoPassenger();

            Assert.Null(place.SmartFortwo.Passenger);
            Assert.Equal(passenger, returnedCrewMember);
        }

        [Fact]
        public void should_disembark_driver_from_smart_fortwo_and_return_it()
        {
            var smartFortwo = new SmartFortwo();
            var driver = new Officer("driver name");
            smartFortwo.EnterDriver(driver);

            var place = new PlaceMock();
            place.SetSmartFortwo(smartFortwo);

            var returnedCrewMember = place.DisembarkSmartFortwoDriver();

            Assert.Null(place.SmartFortwo.Driver);
            Assert.Equal(driver, returnedCrewMember);
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
    }
}
