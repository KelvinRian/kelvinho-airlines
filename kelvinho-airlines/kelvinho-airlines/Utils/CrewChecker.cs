using kelvinho_airlines.Entities.CrewMembers;
using System.Collections.Generic;
using System.Linq;

namespace kelvinho_airlines.Utils
{
    public static class CrewChecker
    {
        public static bool CrewMembersAreAllowedToStayTogether(IEnumerable<CrewMember> crewMembers)
        {
            if (crewMembers.Any(x => x is Prisoner) && !crewMembers.Any(x => x is Policeman))
                return false;

            var crewMembersByType = crewMembers.GroupBy(x => x.GetType());
            foreach(var crewMemberByType in crewMembersByType)
            {
                foreach(var crewMember in crewMembers)
                {
                    if (!crewMemberByType.FirstOrDefault().CanBeTogetherWith(crewMember) && crewMembersByType.Count() == 2)
                        return false;
                }
            }
            return true;
        }
    }
}
