using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.TechnicalCrew
{
    public class PilotTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var pilot = new Pilot("name");
            Assert.Equal("name", pilot.Name);
            Assert.Contains(typeof(Prisoner), pilot.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(FlightAttendant), pilot.IncompatibleCrewMemberTypes);
        }
    }
}
