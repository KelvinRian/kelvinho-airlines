using System.Collections.Generic;
using System.Linq;
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

        public override void Board(IEnumerable<CrewMember> crewMembers)
        {
            CrewMembers.AddRange(crewMembers.Distinct());
        }

        public override void Board(CrewMember crewMember)
            => CrewMembers.Add(crewMember);

        public override void Disembark(List<CrewMember> crewMembers)
            => CrewMembers.RemoveAll(x => crewMembers.Contains(x));


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