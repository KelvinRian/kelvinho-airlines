using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public List<CrewMember> CrewMembers { get; protected set; }

        public Place()
        {
            CrewMembers = new List<CrewMember>();
        }

        public abstract void Board(List<CrewMember> crewMembers);
        public abstract void Disembark(params CrewMember[] crewMembers);
    }
}