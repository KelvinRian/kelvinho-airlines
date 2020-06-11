namespace kelvinho_airlines.Entities
{
    public class FlightServiceChief : CrewMember
    {
        public FlightServiceChief(string name) : base(name)
        {
            CantStayAloneWith.Add(typeof(Prisoner));
            CantStayAloneWith.Add(typeof(Officer));
        }
    }
}