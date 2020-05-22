using System;
using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {
        public Terminal(List<CrewMember> crewMembers) : base()
        {
            Board(crewMembers);
        }

        public Terminal(List<CrewMember> crewMembers, SmartFortwo smartFortwo) : base()
        {
            if (!string.IsNullOrEmpty(smartFortwo.Location))
            {
                throw new ArgumentException("You can't set a smart fortwo that already had a location at constructor");
            }
            SetSmartFortwo(smartFortwo);
            Board(crewMembers);
        }

        public override void Disembark(List<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Remove(crewMember);
            }
        }
    }
}