using kelvinho_airlines.Enums;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class CrewMember : BaseEntity
    {
        public string Name { get; protected set; }
        public CrewType CrewType { get; protected set; }
        public List<Type> CantStayAloneWith { get; protected set; } = new List<Type>();

        public CrewMember(string name) : base()
        {
            Name = name;
            SetCrewType();
        }

        private void SetCrewType()
        {
            if (this is Pilot || this is Officer)
            {
                CrewType = CrewType.Technical;
            }
            else if (this is FlightServiceChief || this is FlightAttendant)
            {
                CrewType = CrewType.Cabin;
            }
            else
            {
                CrewType = CrewType.Common;
            }
        }
    }
}