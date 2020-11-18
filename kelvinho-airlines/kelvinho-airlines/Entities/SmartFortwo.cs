using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public class SmartFortwo
    {
        public CrewMember Driver { get; private set; }
        public CrewMember Passenger { get; private set; }
        public string Location { get; private set; }

        public SmartFortwo()
        {

        }

        public void EnterDriver(CrewMember driver)
        {
            if (driver == null)
                throw new Exception("Its not possible to enter a null driver in the smart fortwo");
            Driver = driver;
        }

        public void EnterPassenger(CrewMember passenger)
        {
            if (passenger == null)
                throw new Exception("Its not possible to enter a null passenger in the smart fortwo");
            Passenger = passenger;
        }

        public void EnterBoth(CrewMember driver, CrewMember passenger)
        {
            EnterDriver(driver);
            EnterPassenger(passenger);
        }

        public IEnumerable<CrewMember> DisembarkAll()
        {
            var driver = DisembarkDriver();
            var passenger = DisembarkPassenger();
            return new List<CrewMember> { driver, passenger };
        }

        public CrewMember DisembarkDriver()
        {
            if (Driver == null)
                throw new Exception("There is no driver in the smart fortwo");

            var driver = Driver;
            Driver = null;
            return driver;
        }

        public CrewMember DisembarkPassenger()
        {
            if (Passenger == null)
                throw new Exception("There is no passenger in the smart fortwo");

            var passenger = Passenger;
            Passenger = null;
            return passenger;
        }

        public void SetLocation(Place place)
            => Location = place.GetType().Name;

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