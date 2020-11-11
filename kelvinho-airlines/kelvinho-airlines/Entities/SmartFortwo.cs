using System;
using System.Collections.Generic;

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

        public void GetIn(Place originPlace, CrewMember driver, CrewMember passenger)
        {
            if (originPlace == null)
                throw new Exception("Place should not be null");

            if (driver != null)
                Driver = driver;

            if (passenger != null)
                Passenger = passenger;

            originPlace.Disembark(new List<CrewMember>() { driver, passenger });
        }

        public IEnumerable<CrewMember> DisembarkAllIn(Place place)
        {
            if (place == null)
                throw new Exception("Place should not be null");

            if (place.SmartFortwo == null)
                throw new Exception("The smart fortwo isn't at the place");

            if (place.SmartFortwo.Driver == null)
                throw new Exception("There is no driver in the smart fortwo");

            if (place.SmartFortwo.Passenger == null)
                throw new Exception("There is no passenger in the smart fortwo");

            var driver = place.SmartFortwo.DisembarkDriver();
            var passenger = place.SmartFortwo.DisembarkPassenger();

            place.Board(new HashSet<CrewMember> { driver, passenger });

            return new List<CrewMember> { driver, passenger };
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