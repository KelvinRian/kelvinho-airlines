using System.Collections.Generic;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public override void Board(List<CrewMember> crewMembers)
        {
            throw new System.NotImplementedException();
        }

        public override void Disembark(params CrewMember[] crewMembers)
        {
            throw new System.NotImplementedException();
        }
    }
}
