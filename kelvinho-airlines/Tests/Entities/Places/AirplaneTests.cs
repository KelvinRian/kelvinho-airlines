using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System.Collections.Generic;
using Xunit;

namespace Tests.Entities.Places
{
    public class AirplaneTests
    {
        [Fact]
        public void to_string_should_show_crew_members_distinguishing_crew_types()
        {
            var technicalMember = new Officer("technicalMember");
            var cabinMember = new FlightAttendant("cabinMember");
            var commonMember = new Policeman("commonMember");

            var airplane = new Airplane();
            airplane.Board(new List<CrewMember> { technicalMember, cabinMember, commonMember });

            Assert.Equal("Airplane:\n\nTechnical Crew:   Officer: technicalMember\n\nCabin Crew:   FlightAttendant: cabinMember\n\nCommon Crew:   Policeman: commonMember\n",
                airplane.ToString());
        }
    }
}
