namespace kelvinho_airlines.Entities
{
    public class FlightServiceChief : CrewMember
    {
        public FlightServiceChief(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(Officer));
        }
    }
}