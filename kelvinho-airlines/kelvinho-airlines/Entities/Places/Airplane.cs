using kelvinho_airlines.Enums;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public override string ToString()
        {
            var technicalCrew = GetStringCrewMembersByType(CrewType.Technical);
            var cabinCrew = GetStringCrewMembersByType(CrewType.Cabin);
            var commonCrew = GetStringCrewMembersByType(CrewType.Common);

            return $"Airplane:\n\nTechnical Crew:   {technicalCrew}\n\nCabin Crew:   {cabinCrew}\n\nCommon Crew:   {commonCrew}\n";
        }

        private string GetStringCrewMembersByType(CrewType type)
        {
            var crewMembers = new StringBuilder();
            foreach (var crewMember in CrewMembers.Where(x => x.CrewType == type))
            {
                if (crewMembers.Length > 0)
                    crewMembers.Append("   |   ");

                crewMembers.Append($"{crewMember.GetType().Name}: {crewMember.Name}");
            }

            return crewMembers.ToString();
        }
    }
}
