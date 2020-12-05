namespace kelvinho_airlines.Entities.CrewMembers
{
    public class FlightAttendant : CabinCrewMember
    {
        public FlightAttendant(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(Pilot));
        }
    }
}