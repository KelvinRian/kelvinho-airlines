﻿using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        private readonly string _nullSmartFortwoException;

        public SmartFortwo SmartFortwo { get; private set; }
        public List<CrewMember> CrewMembers { get; set; }

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

        public CrewMember DisembarkSmartFortwoPassenger()
            => SmartFortwo?.DisembarkPassenger()
                ?? throw new Exception(_nullSmartFortwoException);

        public CrewMember DisembarkSmartFortwoDriver()
            => SmartFortwo?.DisembarkDriver()
                ?? throw new Exception(_nullSmartFortwoException);

        public IEnumerable<CrewMember> DisembarkAllFromSmartFortwo()
            => SmartFortwo?.DisembarkAll()
                ?? throw new Exception(_nullSmartFortwoException);

        public abstract void Board(CrewMember crewMember);
        public abstract void Board(List<CrewMember> crewMembers);
        public abstract void Disembark(List<CrewMember> crewMembers);
    }
}