using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {

        public Terminal(HashSet<CrewMember> crewMembers) : base()
        {
            Board(crewMembers);
        }

        public static Terminal StartWithASmartFortwo(HashSet<CrewMember> crewMembers)
        {
            var terminal = new Terminal(crewMembers);
            terminal.SetSmartFortwo(new SmartFortwo());
            return terminal;
        }

        public override void Board(HashSet<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Add(crewMember);
            }
        }

        public override void Disembark(HashSet<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Remove(crewMember);
            }
        }

        public override string ToString()
        {
            StringBuilder crewMembers = new StringBuilder();
            foreach (var crewMember in CrewMembers)
            {
                crewMembers.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            if (crewMembers.Length == 0)
                return $"Terminal: This place is empty";

            return $"Terminal:\n\n{crewMembers}";
        }
    }
}