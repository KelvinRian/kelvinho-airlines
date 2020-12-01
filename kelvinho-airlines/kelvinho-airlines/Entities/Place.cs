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

        public CrewMember DisembarkSmartFortwoPassenger()
            => SmartFortwo.DisembarkPassenger();

        public CrewMember DisembarkSmartFortwoDriver()
            => SmartFortwo.DisembarkDriver();

        public IEnumerable<CrewMember> DisembarkAllFromSmartFortwo()
            => SmartFortwo.DisembarkAll();

        public abstract void Board(IEnumerable<CrewMember> crewMembers);
        public abstract void Board(CrewMember crewMember);
        public abstract void Disembark(List<CrewMember> crewMembers);
    }
}