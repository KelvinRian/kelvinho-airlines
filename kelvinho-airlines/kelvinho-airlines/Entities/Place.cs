using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.ConstrainedExecution;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public List<CrewMember> CrewMembers { get; protected set; }
        public SmartFortwo SmartFortwo { get; protected set; }

        public Place()
        {
            CrewMembers = new List<CrewMember>();
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

        public abstract void Board(List<CrewMember> crewMembers);

        public abstract void Disembark(params CrewMember[] crewMembers);
    }
}