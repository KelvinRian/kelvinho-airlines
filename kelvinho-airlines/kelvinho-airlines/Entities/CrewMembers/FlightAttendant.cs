namespace kelvinho_airlines.Entities
{
    public class FlightAttendant : CrewMember
    {
        public FlightAttendant(string name) : base(name)
        {
            CantStayAloneWith.Add(typeof(Prisoner));
            CantStayAloneWith.Add(typeof(Pilot));
        }
    }
}