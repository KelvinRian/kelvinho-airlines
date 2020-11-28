using kelvinho_airlines.Utils.ExtensionMethods;
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
        }

        public void RemoveSmartFortwo()
        {
            SmartFortwo = null;
        }

        //TODO null check on SmartFortwo before call GetCrewMembers()
        public IEnumerable<CrewMember> GetSmartFortwoCrewMembers()
            => SmartFortwo.GetCrewMembers();

        //TODO null check on SmartFortwo before call Driver
        public bool SmartFortwoHasDriver()
            => !SmartFortwo.Driver.IsNull();

        public abstract void Board(HashSet<CrewMember> crewMembers);

        public abstract void Disembark(List<CrewMember> crewMembers);
    }
}