namespace kelvinho_airlines.Entities.CrewMembers
{
    public class Pilot : TechnicalCrewMember
    {
        public Pilot(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(FlightAttendant));
        }
    }
}