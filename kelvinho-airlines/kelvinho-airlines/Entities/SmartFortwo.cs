using System;
using System.Collections.Generic;
using System.Linq;

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

        public CrewMember DisembarkDriverIn(Place place)
        {
            if (place == null)
                throw new Exception("Place should not be null");

            if (place.SmartFortwo == null)
                throw new Exception("The smart fortwo isn't at the place");

            if (place.SmartFortwo.Driver == null)
                throw new Exception("There is no driver in the smart fortwo");

            var driver = place.SmartFortwo.DisembarkDriver();
            place.Board(new HashSet<CrewMember> { driver });
            return driver;
        }

        public CrewMember DisembarkPassengerIn(Place place)
        {
            if (place == null)
                throw new Exception("Place should not be null");

            if (place.SmartFortwo == null)
                throw new Exception("The smart fortwo isn't at the place");

            if (place.SmartFortwo.Passenger == null)
                throw new Exception("There is no passenger in the smart fortwo");

            var passenger = place.SmartFortwo.DisembarkPassenger();
            place.Board(new HashSet<CrewMember> { passenger });
            return passenger;
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

        public void Move(Place origin, Place destiny)
        {
            if (origin == null || destiny == null)
                throw new Exception("Origin and destiny should not be null");

            if (origin.SmartFortwo == null)
                throw new Exception("The origin place doesn't have a smart fortwo to move");

            if (origin.SmartFortwo.Driver == null)
                throw new Exception("Smart Fortwo can't move without a driver");

            VerifyCrewMembersMovement(origin.CrewMembers);

            var crewMembersInSmartFortwo = new List<CrewMember> { origin.SmartFortwo.Driver };
            if (origin.SmartFortwo.Passenger != null)
                crewMembersInSmartFortwo.Add(origin.SmartFortwo.Passenger);

            VerifyCrewMembersMovement(crewMembersInSmartFortwo);

            destiny.SetSmartFortwo(origin.SmartFortwo);
            origin.RemoveSmartFortwo();
        }

        private static void VerifyCrewMembersMovement(IEnumerable<CrewMember> crewMembers)
        {
            HashSet<Type> IncompatibleTypesOfCrewMembersAtPlace = new HashSet<Type>();
            HashSet<Type> crewMemberTypesAtPlace = new HashSet<Type>();
            bool hasPoliceman = false;
            bool hasPrisoner = false;

            foreach (var crewMember in crewMembers)
            {
                if (crewMember is Policeman)
                    hasPoliceman = true;

                if (crewMember is Prisoner)
                {
                    hasPrisoner = true;
                }
                else
                {
                    crewMemberTypesAtPlace.Add(crewMember.GetType());
                    IncompatibleTypesOfCrewMembersAtPlace.UnionWith(crewMember.IncompatibleCrewMemberTypes);
                }
            }

            if (hasPrisoner && !hasPoliceman)
                throw new Exception("The prisoner can't stay with the others crew members without a policeman");

            IncompatibleTypesOfCrewMembersAtPlace.IntersectWith(crewMemberTypesAtPlace);

            if (IncompatibleTypesOfCrewMembersAtPlace.Count() == 2 || IncompatibleTypesOfCrewMembersAtPlace.Count() == 1)
            {
                crewMemberTypesAtPlace.ExceptWith(IncompatibleTypesOfCrewMembersAtPlace);
                if (crewMemberTypesAtPlace.Count() == 0)
                {
                    throw new Exception("There is some crew members that cannot be together at the place");
                }
            }
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