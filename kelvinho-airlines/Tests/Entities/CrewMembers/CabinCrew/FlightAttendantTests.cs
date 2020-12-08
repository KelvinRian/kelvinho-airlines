using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.CabinCrew
{
    public class FlightAttendantTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var flightAttendant = new FlightAttendant("name");
            Assert.Equal("name", flightAttendant.Name);
            Assert.Contains(typeof(Prisoner), flightAttendant.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(Pilot), flightAttendant.IncompatibleCrewMemberTypes);
        }
    }
}
