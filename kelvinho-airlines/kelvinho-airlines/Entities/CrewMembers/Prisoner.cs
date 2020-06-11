namespace kelvinho_airlines.Entities
{
    public class Prisoner : CrewMember
    {
        public Prisoner(string name) : base(name)
        {
            CantStayAloneWith.Add(typeof(Officer));
            CantStayAloneWith.Add(typeof(FlightServiceChief));
            CantStayAloneWith.Add(typeof(Pilot));
            CantStayAloneWith.Add(typeof(FlightAttendant));
        }
    }
}