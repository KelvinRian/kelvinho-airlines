using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities
{
    public abstract class Place
    {
        public List<CrewMember> CrewMembers { get; protected set; }
        public SmartFortwo SmartFortwo { get; private set; }

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

        public void Board(List<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Add(crewMember);
            }
        }

        public void Disembark(List<CrewMember> crewMembers)
        {
            foreach (var crewMember in crewMembers)
            {
                CrewMembers.Remove(crewMember);
            }
        }

        public override string ToString()
        {
            StringBuilder crewMembers = new StringBuilder();
            foreach (var crewMember in CrewMembers)
            {
                crewMembers.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            if (crewMembers.Length == 0)
                return $"{GetType().Name}: This place is empty";

            return $"{GetType().Name}:\n\n{crewMembers}";
        }
    }
}