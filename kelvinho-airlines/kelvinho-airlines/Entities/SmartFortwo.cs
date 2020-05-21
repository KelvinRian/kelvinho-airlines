namespace kelvinho_airlines.Entities
{
    public class SmartFortwo
    {
        public CrewMember Driver { get; protected set; }
        public CrewMember Passenger { get; protected set; }
        public string Location { get; set; }

        public SmartFortwo() : base()
        {

        }

        public void Board(CrewMember driver, CrewMember passenger)
        {
            if (driver != null)
            {
                Driver = driver;
            }

            if (passenger != null)
            {
                Passenger = passenger;
            }
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

        public override string ToString()
        {
            return $"Driver: {Driver.Name}\nPassenger: {Passenger.Name}\nLocation: {Location}";
        }
    }
}