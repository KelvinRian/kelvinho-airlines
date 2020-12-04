using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System.Collections.Generic;
using Xunit;

namespace Tests.Entities.Places
{
    public class TerminalTests
    {
        [Fact]
        public void should_create_a_terminal_with_a_smart_fortwo()
        {
            var officer = new Officer("officer");
            var pilot = new Pilot("pilot");
            var crewMembers = new List<CrewMember>
            {
                officer,
                pilot
            };

            var terminal = Terminal.CreateWithSmartFortwo(crewMembers);

            Assert.Contains(officer, terminal.CrewMembers);
            Assert.Contains(pilot, terminal.CrewMembers);
            Assert.NotNull(terminal.SmartFortwo);
        }

        [Fact]
        public void to_string_method_should_write_empty_when_there_is_no_crew_member()
        {
            var terminal = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            Assert.Equal("Terminal: Empty\n", terminal.ToString());
        }

        [Fact]
        public void to_string_method_should_write_crew_members_at_terminal()
        {
            var officer = new Officer("officer");
            var pilot = new Pilot("pilot");
            var crewMembers = new List<CrewMember>
            {
                officer,
                pilot
            };

            var terminal = Terminal.CreateWithSmartFortwo(crewMembers);

            Assert.Equal("Terminal:\n\nOfficer : officer\r\nPilot : pilot\r\n", terminal.ToString());
        }
    }
}