using kelvinho_airlines.Entities.CrewMembers;
using System;

namespace Tests.Mocks
{
    public class CrewMemberMock : CrewMember
    {
        public CrewMemberMock(string name) : base(name)
        {
        }

        public void AddIncompatibleCrewMemberType(Type crewMemberType)
        {
            IncompatibleCrewMemberTypes.Add(crewMemberType);
        }
    }
}
