using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public List<CrewMember> CrewMembers { get; protected set; }

        public abstract void Board(params CrewMember[] crewMembers);
        public abstract void Disembark(params CrewMember[] crewMembers);
    }
}