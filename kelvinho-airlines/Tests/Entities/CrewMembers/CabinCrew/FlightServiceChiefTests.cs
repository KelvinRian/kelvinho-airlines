using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.CabinCrew
{
    public class FlightServiceChiefTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var flightServiceChief = new FlightServiceChief("name");
            Assert.Equal("name", flightServiceChief.Name);
            Assert.Contains(typeof(Prisoner), flightServiceChief.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(Officer), flightServiceChief.IncompatibleCrewMemberTypes);
        }
    }
}
