using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.TechnicalCrew
{
    public class OfficerTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var officer = new Officer("name");
            Assert.Equal("name", officer.Name);
            Assert.Contains(typeof(Prisoner), officer.IncompatibleCrewMemberTypes);
            Assert.Contains(typeof(FlightServiceChief), officer.IncompatibleCrewMemberTypes);
        }
    }
}
