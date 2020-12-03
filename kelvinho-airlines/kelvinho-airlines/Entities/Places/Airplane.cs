using kelvinho_airlines.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public Airplane()
        {
        }

        public override void Board(List<CrewMember> crewMembers)
        {
            CrewMembers.AddRange(crewMembers.Distinct());
        }

        public override void Disembark(List<CrewMember> crewMembers)
        {
            CrewMembers.RemoveAll(x => crewMembers.Contains(x));
        }

        public override string ToString()
        {
            StringBuilder technicalCrew = new StringBuilder();
            foreach (var crewMember in CrewMembers.Where(x => x.CrewType == CrewType.Technical))
            {
                if (technicalCrew.Length > 0)
                    technicalCrew.Append("   |   ");

                technicalCrew.Append($"{crewMember.GetType().Name}: {crewMember.Name}");
            }

            StringBuilder cabinCrew = new StringBuilder();
            foreach (var crewMember in CrewMembers.Where(x => x.CrewType == CrewType.Cabin))
            {
                if (cabinCrew.Length > 0)
                    cabinCrew.Append("  |   ");

                cabinCrew.Append($"{crewMember.GetType().Name}: {crewMember.Name}");
            }

            StringBuilder commonCrew = new StringBuilder();
            foreach (var crewMember in CrewMembers.Where(x => x.CrewType == CrewType.Common))
            {
                if (commonCrew.Length > 0)
                    commonCrew.Append("   |   ");
                commonCrew.Append($"{crewMember.GetType().Name}: {crewMember.Name}");
            }

            return $"Airplane:\n\nTechnical Crew:   {technicalCrew}\n\nCabin Crew:   {cabinCrew}\n\nCommon Crew:   {commonCrew}\n";
        }

        public override void Board(CrewMember crewMember)
        {
            CrewMembers.Add(crewMember);
        }
    }
}
