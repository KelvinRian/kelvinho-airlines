using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public SmartFortwo SmartFortwo { get; private set; }
        public HashSet<CrewMember> CrewMembers { get; set; }

        public Place()
        {
            CrewMembers = new HashSet<CrewMember>();
        }

        public void SetSmartFortwo(SmartFortwo smartFortwo)
        {
            SmartFortwo = smartFortwo;
            SmartFortwo.Location = GetType().Name;
        }

        public void RemoveSmartFortwo()
        {
            SmartFortwo = null;
        }

        public abstract void Board(HashSet<CrewMember> crewMembers);

        public abstract void Disembark(List<CrewMember> crewMembers);
    }
}