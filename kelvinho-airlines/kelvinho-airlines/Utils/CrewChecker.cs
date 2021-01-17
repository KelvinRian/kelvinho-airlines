using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Utils.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;

namespace kelvinho_airlines.Utils
{
    public static class CrewChecker
    {
        public static bool CrewMembersAreAllowedToStayTogether(IEnumerable<CrewMember> crewMembers)
        {
            if (crewMembers.IsNull())
                return true;
            else
                return !IsThereAPrisonerWithoutPoliceman(crewMembers)
                    && !IncompatibleCrewMembersAreTogetherAlone(crewMembers);
        }

        private static bool IsThereAPrisonerWithoutPoliceman(IEnumerable<CrewMember> crewMembers)
        {
                var hasPrisoner = crewMembers.Where(x => !x.IsNull()).Any(x => x is Prisoner);
                var hasPoliceman = crewMembers.Where(x => !x.IsNull()).Any(x => x is Policeman);
                return hasPrisoner && !hasPoliceman;
        }

        private static bool IncompatibleCrewMembersAreTogetherAlone(IEnumerable<CrewMember> crewMembers)
        {
            var crewMembersByType = crewMembers.Where(x => !x.IsNull()).GroupBy(x => x.GetType());

            foreach (var crewMember in crewMembers.Where(x => !x.IsNull()))
            {
                foreach (var crewMemberGrouping in crewMembersByType)
                {
                    var firstCrewMemberOfGrouping = crewMemberGrouping.FirstOrDefault();
                    var theyAreTheOnlyTypesAtTheLocation = crewMembersByType.Count() == 2;

                    if (!crewMember.CanBeTogetherWith(firstCrewMemberOfGrouping) && theyAreTheOnlyTypesAtTheLocation)
                        return true;
                }
            }

            return false;
        }
    }
}
