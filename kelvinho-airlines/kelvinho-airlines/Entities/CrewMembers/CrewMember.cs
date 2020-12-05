using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities.CrewMembers
{
    public abstract class CrewMember
    {
        public string Name { get; protected set; }
        public HashSet<Type> IncompatibleCrewMemberTypes { get; protected set; } = new HashSet<Type>();

        public CrewMember(string name) : base()
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Name}";
        }
    }
}