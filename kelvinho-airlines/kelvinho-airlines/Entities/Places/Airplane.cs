using kelvinho_airlines.Entities.CrewMembers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public override string ToString()
        {
            var technicalCrewMembers = GetCrewMembersInfo(CrewMembers.Where(x => x is TechnicalCrewMember));
            var cabinCrewMembers = GetCrewMembersInfo(CrewMembers.Where(x => x is CabinCrewMember));
            var commonCrewMembers = GetCrewMembersInfo(CrewMembers.Where(x => x is CommonCrewMember));


            return $"Airplane:\n\nTechnical Crew:   {technicalCrewMembers}\n\nCabin Crew:   {cabinCrewMembers}\n\nCommon Crew:   {commonCrewMembers}\n";
        }

        private string GetCrewMembersInfo(IEnumerable<CrewMember> crewMembers)
        {
            var crewMembersInfo = new StringBuilder();
            foreach (var crewMember in crewMembers)
            {
                if (crewMembersInfo.Length > 0)
                    crewMembersInfo.Append("   |   ");

                crewMembersInfo.Append($"{crewMember.GetType().Name}: {crewMember.Name}");
            }

            return crewMembersInfo.ToString();
        }
    }
}
