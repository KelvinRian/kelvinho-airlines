using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using System.Collections.Generic;
using Xunit;

namespace Tests.Entities.Places
{
    public class AirplaneTests
    {
        [Fact]
        public void To_string_should_show_crew_members_distinguishing_crew_types()
        {
            var technicalMember = new Officer("technicalMember");
            var cabinMember = new FlightAttendant("cabinMember");
            var commonMember = new Policeman("commonMember");

            var airplane = new Airplane();
            airplane.Board(new List<CrewMember> { technicalMember, cabinMember, commonMember });

            Assert.Equal("Airplane:\n\nTechnical Crew:   Officer: technicalMember\n\nCabin Crew:   FlightAttendant: cabinMember\n\nCommon Crew:   Policeman: commonMember\n",
                airplane.ToString());
        }

        [Fact]
        public void Should_append_bar_when_read_the_second_crew_member()
        {
            var technicalMembers = new List<CrewMember> {
                new Officer("technicalMember"),
                new Officer("otherMember"),
            };
            var cabinMember = new FlightAttendant("cabinMember");
            var commonMember = new Policeman("commonMember");

            var airplane = new Airplane();
            airplane.Board(new List<CrewMember> { cabinMember, commonMember });
            airplane.Board(technicalMembers);

            Assert.Equal("Airplane:\n\nTechnical Crew:   Officer: technicalMember   |   Officer: otherMember\n\nCabin Crew:   FlightAttendant: cabinMember\n\nCommon Crew:   Policeman: commonMember\n",
                airplane.ToString());
        }
    }
}
