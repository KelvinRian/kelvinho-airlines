using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities.CrewMembers
{
    public abstract class CrewMember
    {
        public string Name { get; private set; }
        public List<Type> IncompatibleCrewMemberTypes { get; protected set; } = new List<Type>();

        public CrewMember(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Name}";
        }
    }
}