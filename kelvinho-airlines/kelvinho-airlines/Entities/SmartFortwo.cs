namespace kelvinho_airlines.Entities
{
    public class SmartFortwo
    {
        public CrewMember Driver { get; protected set; }
        public CrewMember Passenger { get; protected set; }
        public Place Location { get; protected set; }

        public SmartFortwo(Place location)
        {
            Location = location;
        }

        public void Move(Place location)
        {
            
        }
    }
}
