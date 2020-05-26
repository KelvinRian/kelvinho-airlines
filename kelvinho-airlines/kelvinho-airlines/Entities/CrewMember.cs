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

            if (GetType().Name == "Pilot" || GetType().Name == "Officer")
            {
                CrewType = CrewType.Technical;
            }
            else if (GetType().Name == "FlightServiceChief" || GetType().Name == "FlightAttendant")
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