using kelvinho_airlines.Entities.CrewMembers;
using Xunit;

namespace Tests.Entities.CrewMembers.CommonCrew
{
    public class PolicemanTests
    {
        [Fact]
        public void constructor_should_set_name_and_incompatible_types()
        {
            var policeman = new Policeman("name");
            Assert.Equal("name", policeman.Name);
            Assert.Empty(policeman.IncompatibleCrewMemberTypes);
        }
    }
}
