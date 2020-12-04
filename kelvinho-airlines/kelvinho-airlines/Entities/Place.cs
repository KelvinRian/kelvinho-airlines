using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        protected readonly string _nullSmartFortwoException;

        public SmartFortwo SmartFortwo { get; private set; }
        public List<CrewMember> CrewMembers { get; private set; }

        public Place()
        {
            CrewMembers = new List<CrewMember>();
            _nullSmartFortwoException = $"{GetType().Name} does not have a smart fortwo";
        }

        public void SetSmartFortwo(SmartFortwo smartFortwo)
            => SmartFortwo = smartFortwo;

        public void RemoveSmartFortwo()
            => SmartFortwo = null;

        public List<CrewMember> GetSmartFortwoCrewMembers()
            => SmartFortwo?.GetCrewMembers()
                ?? throw new Exception(_nullSmartFortwoException);

        public bool SmartFortwoHasDriver()
            => !SmartFortwo?.Driver.IsNull() 
                ?? throw new Exception(_nullSmartFortwoException);

        public CrewMember DisembarkPassengerFromSmartFortwo()
            => SmartFortwo?.DisembarkPassenger()
                ?? throw new Exception(_nullSmartFortwoException);

        public CrewMember DisembarkDriverFromSmartFortwo()
            => SmartFortwo?.DisembarkDriver()
                ?? throw new Exception(_nullSmartFortwoException);

        public IEnumerable<CrewMember> DisembarkAllFromSmartFortwo()
            => SmartFortwo?.DisembarkAll()
                ?? throw new Exception(_nullSmartFortwoException);

        public void Board(CrewMember crewMembers)
            => CrewMembers.Add(crewMembers);

        public void Board(List<CrewMember> crewMembers)
            => CrewMembers.AddRange(crewMembers);

        public void Remove(List<CrewMember> crewMembers)
            => CrewMembers.RemoveAll(x => crewMembers.Contains(x));

    }
}