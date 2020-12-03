using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using Xunit;

namespace Tests.Entities.Places
{
    public class AirplaneTests
    {
        [Fact]
        public void should_board_a_single_crew_member()
        {
            var airplane = new Airplane();
            var crewMember = new FlightAttendant("crew member name");
            airplane.Board(crewMember);

            Assert.Contains(crewMember, airplane.CrewMembers);
        }
    }
}
