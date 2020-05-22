using System.Collections.Generic;

namespace kelvinho_airlines.Entities.Places
{
    public class Airplane : Place
    {
        public Airplane() : base()
        {

        }

        public override void Disembark(List<CrewMember> crewMembers)
        {
            throw new System.NotImplementedException();
        }
    }
}
