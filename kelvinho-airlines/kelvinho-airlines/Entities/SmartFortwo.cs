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

        //TODO
        //Methods:
        //EnterDriver
        //EnterPassenger
        //EnterAll
        //use exceptions when some argument is null
        //Remove GetIn method

        public void GetIn(CrewMember driver, CrewMember passenger)
        {
            if (driver != null)
                Driver = driver;

            if (passenger != null)
                Passenger = passenger;
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