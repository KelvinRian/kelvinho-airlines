using kelvinho_airlines.Entities.CrewMembers;
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

        public void Board(CrewMember crewMember)
            => CrewMembers.Add(crewMember);

        public void Board(List<CrewMember> crewMembers)
            => CrewMembers.AddRange(crewMembers);

        public void Remove(params CrewMember[] crewMembers)
        {
            foreach (var crewMember in crewMembers)
                CrewMembers.Remove(crewMember);
        }

        public void PutDriverInSmartFortwo(CrewMember driver)
        {
            if(SmartFortwo.IsNull())
                throw new Exception(_nullSmartFortwoException);

            SmartFortwo.EnterDriver(driver);
        }

        public void PutPassengerInSmartFortwo(CrewMember passenger)
        {
            if (SmartFortwo.IsNull())
                throw new Exception(_nullSmartFortwoException);

            SmartFortwo.EnterPassenger(passenger);
        }

        public void PutBothInSmartFortwo(CrewMember driver, CrewMember passenger)
        {
            if (SmartFortwo.IsNull())
                throw new Exception(_nullSmartFortwoException);

            SmartFortwo.EnterDriver(driver);
            SmartFortwo.EnterPassenger(passenger);
        }
    }
}