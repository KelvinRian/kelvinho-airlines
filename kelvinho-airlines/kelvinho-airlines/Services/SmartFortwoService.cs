using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Services
{
    public class SmartFortwoService : ISmartFortwoService
    {
        private List<string> drivers;
        
        public SmartFortwoService()
        {
            drivers = new List<string>()
            {
                "Pilot",
                "Policeman",
                "FlightServiceChief"
            };
        }

        public void Board(Place place, CrewMember driver, CrewMember passenger)
        {
            if (place == null)
                throw new ArgumentException("Place should not be null");

            if (place.SmartFortwo == null)
                throw new ArgumentException("This place doesn't have a smart fortwo to board");

            var crewMembers = new HashSet<CrewMember>
            {
                driver,
                passenger
            };

            place.Disembark(crewMembers);
            place.SmartFortwo.Board(driver, passenger);
        }

        public void DisembarkDriver(Place place)
        {
            if (place == null)
                throw new ArgumentException("Place should not be null");

            if (place.SmartFortwo == null)
                throw new ArgumentException("The smart fortwo isn't at the place");

            if (place.SmartFortwo.Driver == null)
                throw new ArgumentException("There is no driver in the smart fortwo");

            var driver = place.SmartFortwo.DisembarkDriver();
            place.Board(new HashSet<CrewMember> { driver });
        }

        public void DisembarkPassenger(Place place)
        {
            if (place == null)
                throw new ArgumentException("Place should not be null");

            if (place.SmartFortwo == null)
                throw new ArgumentException("The smart fortwo isn't at the place");

            if (place.SmartFortwo.Passenger == null)
                throw new ArgumentException("There is no passenger in the smart fortwo");

            var passenger = place.SmartFortwo.DisembarkPassenger();
            place.Board(new HashSet<CrewMember> { passenger });
        }

        public void Move(Place origin, Place destiny)
        {
            if (origin == null || destiny == null)
                throw new ArgumentException("Origin and destiny should not be null");

            if (origin.SmartFortwo == null)
                throw new ArgumentException("The origin place doesn't have a smart fortwo to move");

            if (origin.SmartFortwo.Driver == null)
                throw new ArgumentException("Smart Fortwo can't move without a driver");

            if (!drivers.Contains(origin.SmartFortwo.Driver.GetType().Name))
                throw new ArgumentException($"{origin.SmartFortwo.Driver.Name} is not authorized to drive this vehicle");

            destiny.SetSmartFortwo(origin.SmartFortwo);
            origin.RemoveSmartFortwo();
        }

        private void VerifyCrewMembersMovement(Place place)
        {
            //TODO
            //To verify in this method wheter there are crewmbers that cannot be together in the place and in the smartfortwo
        }
    }
}
