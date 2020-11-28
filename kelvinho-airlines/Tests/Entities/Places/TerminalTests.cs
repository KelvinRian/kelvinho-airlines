using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System.Collections.Generic;
using Xunit;

namespace Tests.Entities.Places
{
    public class TerminalTests
    {
        [Fact]
        public void should_board_a_single_crew_member()
        {
            var terminal = new Terminal(new HashSet<CrewMember>());
            var crewMember = new Pilot("crew member name");
            terminal.Board(crewMember);
            Assert.Contains(crewMember, terminal.CrewMembers);
        }
    }
}
