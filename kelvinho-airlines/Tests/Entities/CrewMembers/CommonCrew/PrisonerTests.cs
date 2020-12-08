using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.CommonCrew
{
    public class PrisonerTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var prisoner = new Prisoner("name");

            Assert.Equal("name", prisoner.Name);
            Assert.Contains(typeof(Officer), prisoner.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(FlightServiceChief), prisoner.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(Pilot), prisoner.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(FlightAttendant), prisoner.IncompatibleCrewMemberTypes);
        }
    }
}
