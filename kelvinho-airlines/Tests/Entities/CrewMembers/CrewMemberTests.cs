using kelvinho_airlines.Entities.CrewMembers;
using Tests.Mocks;
using Xunit;

namespace Tests.Entities.CrewMembers
{
    public class CrewMemberTests
    {
        [Fact]
        public void Constructor_should_set_name()
        {
            var creMember = new CrewMemberMock("name");
            Assert.Equal("name", creMember.Name);
        }

        [Fact]
        public void To_string_should_show_type_and_name()
        {
            var crewMember = new CrewMemberMock("name");
            Assert.Equal($"{crewMember.GetType().Name}: {crewMember.Name}", crewMember.ToString());
        }

        [Fact]
        public void Should_return_true_when_a_crew_member_can_be_together_with_other()
        {
            var crewMember = new CrewMemberMock("name");
            var result = crewMember.CanBeTogetherWith(new Pilot("pilot"));
            Assert.True(result);
        }

        [Fact]
        public void Should_return_false_when_a_crew_member_cannot_be_together_with_other()
        {
            var crewMember = new CrewMemberMock("name");
            crewMember.AddIncompatibleCrewMemberType(typeof(Pilot));
            var result = crewMember.CanBeTogetherWith(new Pilot("pilot"));
            Assert.False(result);
        }
    }
}
