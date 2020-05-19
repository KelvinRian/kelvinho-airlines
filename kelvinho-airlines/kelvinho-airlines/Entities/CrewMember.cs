using System;

namespace kelvinho_airlines.Entities
{
    public abstract class CrewMember : BaseEntity
    {
        public string Name { get; set; }

        public CrewMember(string name) : base()
        {
            Name = name;
        }
    }
}