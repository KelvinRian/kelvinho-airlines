namespace kelvinho_airlines.Entities.CrewMembers
{
    public class FlightServiceChief : CabinCrewMember
    {
        public FlightServiceChief(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(Officer));
        }
    }
}