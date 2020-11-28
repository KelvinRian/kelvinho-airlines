using kelvinho_airlines.Entities;
using System.Collections.Generic;

namespace Tests.Mocks
{
    public class PlaceMock : Place
    {
        public override void Board(IEnumerable<CrewMember> crewMembers)
        {
            throw new System.NotImplementedException();
        }

        public override void Board(CrewMember crewMember)
        {
            throw new System.NotImplementedException();
        }

        public override void Disembark(List<CrewMember> crewMembers)
        {
            throw new System.NotImplementedException();
        }
    }
}
