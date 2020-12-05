using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Entities
{
    public class SmartFortwo
    {
        public CrewMember Driver { get; private set; }
        public CrewMember Passenger { get; private set; }

        public void EnterDriver(CrewMember driver)
            => Driver = driver ?? throw new Exception("Its not possible to enter a null driver in the smart fortwo");

        public void EnterPassenger(CrewMember passenger)
            => Passenger = passenger ?? throw new Exception("Its not possible to enter a null passenger in the smart fortwo");

        public void EnterBoth(CrewMember driver, CrewMember passenger)
        {
            EnterDriver(driver);
            EnterPassenger(passenger);
        }

        public CrewMember DisembarkDriver()
        {
            if (Driver.IsNull())
                throw new Exception("There is no driver in the smart fortwo");

            var driver = Driver;
            Driver = null;
            return driver;
        }

        public CrewMember DisembarkPassenger()
        {
            if (Passenger.IsNull())
                throw new Exception("There is no passenger in the smart fortwo");

            var passenger = Passenger;
            Passenger = null;
            return passenger;
        }

        public IEnumerable<CrewMember> DisembarkAll()
        {
            var driver = DisembarkDriver();
            var passenger = DisembarkPassenger();
            return new List<CrewMember> { driver, passenger };
        }

        public List<CrewMember> GetCrewMembers()
            => new List<CrewMember> { Driver, Passenger };

        public override string ToString()
        {
            var driver = !Driver.IsNull() ? Driver.Name : "Empty";
            var driverType = !Driver.IsNull() ? Driver.GetType().Name : "";

            var passenger = !Passenger.IsNull() ? Passenger.Name : "Empty";
            var passengerType = !Passenger.IsNull() ? Passenger.GetType().Name : "";

            return $"Smart Fortwo:   |   Driver: {driverType} {driver}   |   Passenger: {passengerType} {passenger}";
        }
    }
}