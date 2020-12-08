using Tests.Mocks;
using Xunit;

namespace Tests.Entities.CrewMembers
{
    public class CrewMemberTests
    {
        [Fact]
        public void constructor_should_set_name()
        {
            var creMember = new CrewMemberMock("name");
            Assert.Equal("name", creMember.Name);
        }

        [Fact]
        public void to_string_should_show_type_and_name()
        {
            var crewMember = new CrewMemberMock("name");
            Assert.Equal($"{crewMember.GetType().Name}: {crewMember.Name}", crewMember.ToString());
        }
    }
}
