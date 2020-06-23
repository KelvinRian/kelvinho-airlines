namespace kelvinho_airlines.Entities
{
    public class Officer : CrewMember
    {
        public Officer(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(FlightServiceChief));
        }
    }
}