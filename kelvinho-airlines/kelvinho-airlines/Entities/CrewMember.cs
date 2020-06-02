using kelvinho_airlines.Enums;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class CrewMember : BaseEntity
    {
        public string Name { get; protected set; }
        public CrewType CrewType { get; protected set; }
        public List<string> CantBeAloneWith { get; protected set; } = new List<string>();

        public CrewMember(string name) : base()
        {
            Name = name;
            SetCrewType();
            SetCantBeAloneWith();
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

        private void SetCantBeAloneWith()
        {
            if (!(this is Prisoner) && !(this is Policeman))
            {
                CantBeAloneWith.Add("Prisoner");
            }
            else if (this is Prisoner)
            {
                var crewMembers = new List<string>()
                {
                    "Officer",
                    "FlightServiceChief",
                    "Pilot",
                    "FlightAttendant"
                };
                CantBeAloneWith.AddRange(crewMembers);
            }
            else if (this is Officer)
            {
                CantBeAloneWith.Add("FlightServiceChief");
            }
            else if (this is FlightServiceChief)
            {
                CantBeAloneWith.Add("Officer");
            }
            else if (this is Pilot)
            {
                CantBeAloneWith.Add("FlightAttendant");
            }
            else if (this is FlightAttendant)
            {
                CantBeAloneWith.Add("Pilot");
            }
        }
    }
}