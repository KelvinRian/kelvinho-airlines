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

        public void Board(CrewMember? driver, CrewMember? passenger)
        {

        }

        public void DisembarkDriver(CrewMember driver)
        {

        }

        public void DisembarkPassenger(CrewMember passenger)
        {

        }

        public void Move(Place location)
        {

        }
    }
}