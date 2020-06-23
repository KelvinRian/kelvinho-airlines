namespace kelvinho_airlines.Entities
{
    public class SmartFortwo : BaseEntity
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

        public CrewMember DisembarkDriver()
        {
            var driver = Driver;
            Driver = null;
            return driver;
        }

        public CrewMember DisembarkPassenger()
        {
            var passenger = Passenger;
            Passenger = null;
            return passenger;
        }

        public override string ToString()
        {
            var driver = Driver != null ? Driver.Name : "Empty";
            var driverType = Driver != null ? Driver.GetType().Name : "";

            var passenger = Passenger != null ? Passenger.Name : "Empty";
            var passengerType = Passenger != null ? Passenger.GetType().Name : "";

            return $"Smart Fortwo:   |   Driver: {driverType} {driver}   |   Passenger: {passengerType} {passenger}   |   Location: {Location}";
        }
    }
}