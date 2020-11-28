using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using Xunit;

namespace Tests.Entities.Places
{
    public class AirplaneTests
    {
        [Fact]
        public void should_board_a_single_crew_member_and_put_him_in_the_cabin_crew()
        {
            var airplane = new Airplane();
            var crewMember = new FlightAttendant("crew member name");
            airplane.Board(crewMember);

            Assert.Contains(crewMember, airplane.CrewMembers);
            Assert.Contains(crewMember, airplane.CabinCrew);
            Assert.DoesNotContain(crewMember, airplane.TechnicalCrew);
            Assert.DoesNotContain(crewMember, airplane.CommonCrew);
        }

        [Fact]
        public void should_board_a_single_crew_member_and_put_him_in_the_technical_crew()
        {
            var airplane = new Airplane();
            var crewMember = new Pilot("crew member name");
            airplane.Board(crewMember);

            Assert.Contains(crewMember, airplane.CrewMembers);
            Assert.Contains(crewMember, airplane.TechnicalCrew);
            Assert.DoesNotContain(crewMember, airplane.CabinCrew);
            Assert.DoesNotContain(crewMember, airplane.CommonCrew);
        }

        [Fact]
        public void should_board_a_single_crew_member_and_put_him_in_the_common_crew()
        {
            var airplane = new Airplane();
            var crewMember = new Policeman("crew member name");
            airplane.Board(crewMember);

            Assert.Contains(crewMember, airplane.CrewMembers);
            Assert.Contains(crewMember, airplane.CommonCrew);
            Assert.DoesNotContain(crewMember, airplane.CabinCrew);
            Assert.DoesNotContain(crewMember, airplane.TechnicalCrew);
        }
    }
}
