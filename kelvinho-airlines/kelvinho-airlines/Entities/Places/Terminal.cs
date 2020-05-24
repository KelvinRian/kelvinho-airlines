using System.Collections.Generic;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {
        public Terminal(List<CrewMember> crewMembers) : base()
        {
            Board(crewMembers);
        }

        public static Terminal StartWithASmartFortwo(List<CrewMember> crewMembers)
        {
            var terminal = new Terminal(crewMembers);
            terminal.SetSmartFortwo(new SmartFortwo());
            return terminal;
        }
    }
}