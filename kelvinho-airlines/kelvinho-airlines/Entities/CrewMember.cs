using kelvinho_airlines.Enums;

namespace kelvinho_airlines.Entities
{
    public abstract class CrewMember : BaseEntity
    {
        public string Name { get; protected set; }
        public CrewType CrewType { get; protected set; }

        public CrewMember(string name) : base()
        {
            Name = name;

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