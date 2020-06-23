namespace kelvinho_airlines.Entities
{
    public class Pilot : CrewMember
    {
        public Pilot(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(FlightAttendant));
        }
    }
}