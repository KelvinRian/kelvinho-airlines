namespace kelvinho_airlines.Entities
{
    public class FlightAttendant : CrewMember
    {
        public FlightAttendant(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(Pilot));
        }
    }
}