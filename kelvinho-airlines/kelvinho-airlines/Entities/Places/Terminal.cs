using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {
        private Terminal(List<CrewMember> crewMembers)
        {
            Board(crewMembers);
        }

        public static Terminal CreateWithSmartFortwo(List<CrewMember> crewMembers)
        {
            var terminal = new Terminal(crewMembers);
            terminal.SetSmartFortwo(new SmartFortwo());
            return terminal;
        }

        public override string ToString()
        {
            StringBuilder crewMembers = new StringBuilder();
            foreach (var crewMember in CrewMembers)
            {
                crewMembers.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            if (crewMembers.Length == 0)
                return $"Terminal: Empty\n";

            return $"Terminal:\n\n{crewMembers}";
        }
    }
}