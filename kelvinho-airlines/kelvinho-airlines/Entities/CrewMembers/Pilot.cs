namespace kelvinho_airlines.Entities
{
    public class Pilot : CrewMember
    {
        public Pilot(string name) : base(name)
        {
            CantStayAloneWith.Add(typeof(Prisoner));
            CantStayAloneWith.Add(typeof(FlightAttendant));
        }
    }
}