namespace kelvinho_airlines.Entities.CrewMembers
{
    public class Officer : TechnicalCrewMember
    {
        public Officer(string name) : base(name)
        {
            IncompatibleCrewMemberTypes.Add(typeof(Prisoner));
            IncompatibleCrewMemberTypes.Add(typeof(FlightServiceChief));
        }
    }
}