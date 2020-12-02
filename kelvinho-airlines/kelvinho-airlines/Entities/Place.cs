using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public SmartFortwo SmartFortwo { get; private set; }
        public List<CrewMember> CrewMembers { get; set; }

        public Place()
        {
            CrewMembers = new List<CrewMember>();
        }

        public void SetSmartFortwo(SmartFortwo smartFortwo)
            => SmartFortwo = smartFortwo;

        public void RemoveSmartFortwo()
            => SmartFortwo = null;

        public List<CrewMember> GetSmartFortwoCrewMembers()
            => SmartFortwo?.GetCrewMembers()
                ?? throw new Exception($"{GetType().Name} does not have a smart fortwo");

        public bool SmartFortwoHasDriver()
            => !SmartFortwo?.Driver.IsNull() 
                ?? throw new Exception($"{GetType().Name} does not have a smart fortwo");

        public CrewMember DisembarkSmartFortwoPassenger()
            => SmartFortwo?.DisembarkPassenger()
                ?? throw new Exception($"{GetType().Name} does not have a smart fortwo");

        public CrewMember DisembarkSmartFortwoDriver()
            => SmartFortwo?.DisembarkDriver()
                ?? throw new Exception($"{GetType().Name} does not have a smart fortwo");

        public IEnumerable<CrewMember> DisembarkAllFromSmartFortwo()
            => SmartFortwo?.DisembarkAll()
                ?? throw new Exception($"{GetType().Name} does not have a smart fortwo");

        public abstract void Board(IEnumerable<CrewMember> crewMembers);
        public abstract void Board(CrewMember crewMember);
        public abstract void Disembark(List<CrewMember> crewMembers);
    }
}