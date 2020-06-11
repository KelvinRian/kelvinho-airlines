namespace kelvinho_airlines.Entities
{
    public class Officer : CrewMember
    {
        public Officer(string name) : base(name)
        {
            CantStayAloneWith.Add(typeof(Prisoner));
            CantStayAloneWith.Add(typeof(FlightServiceChief));
        }
    }
}