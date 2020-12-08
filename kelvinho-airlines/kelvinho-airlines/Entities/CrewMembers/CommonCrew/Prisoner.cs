namespace kelvinho_airlines.Entities.CrewMembers
{
    public class Prisoner : CommonCrewMember
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