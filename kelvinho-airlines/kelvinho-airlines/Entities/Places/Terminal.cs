using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {
        public List<CrewMember> CrewMembers { get; set; }

        public Terminal(List<CrewMember> crewMembers) : base()
        {
            CrewMembers = new List<CrewMember>();
            Board(crewMembers);
        }

        public static Terminal StartWithASmartFortwo(List<CrewMember> crewMembers)
        {
            var terminal = new Terminal(crewMembers);
            terminal.SetSmartFortwo(new SmartFortwo());
            return terminal;
        }

        public override void Board(List<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Add(crewMember);
            }
        }

        public override void Disembark(List<CrewMember> crewMembers)
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