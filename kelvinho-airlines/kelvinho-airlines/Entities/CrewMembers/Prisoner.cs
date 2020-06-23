namespace kelvinho_airlines.Entities
{
    public class Prisoner : CrewMember
    {
        public Prisoner(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Officer));
            IncompatibleCrewMemberTypes.Add(typeof(FlightServiceChief));
            IncompatibleCrewMemberTypes.Add(typeof(Pilot));
            IncompatibleCrewMemberTypes.Add(typeof(FlightAttendant));
        }
    }
}