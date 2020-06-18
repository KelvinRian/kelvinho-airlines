using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Services
{
    public class SmartFortwoService : ISmartFortwoService
    {
        private readonly List<Type> _drivers;

        public SmartFortwoService(List<Type> drivers)
        {
            _drivers = drivers;
        }

        public void Board(Place originPlace, CrewMember driver, CrewMember passenger)
        {
            if (originPlace == null)
                throw new ArgumentException("Place should not be null");

            if (originPlace.SmartFortwo == null)
                throw new ArgumentException("This place doesn't have a smart fortwo to board");

            if (!_drivers.Contains(driver.GetType()))
                throw new ArgumentException($"{driver.Name} is not authorized to drive this vehicle");

            var crewMembers = new HashSet<CrewMember>
            {
                driver,
                passenger
            };

            originPlace.Disembark(crewMembers);
            originPlace.SmartFortwo.Board(driver, passenger);
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

            //Refatorar
            VerifyCrewMembersMovement(origin.CrewMembers);

            var crewMembersInSmartFortwo = new List<CrewMember> { origin.SmartFortwo.Driver };
            if (origin.SmartFortwo.Passenger != null)
                crewMembersInSmartFortwo.Add(origin.SmartFortwo.Passenger);

            VerifyCrewMembersMovement(crewMembersInSmartFortwo);

            destiny.SetSmartFortwo(origin.SmartFortwo);
            origin.RemoveSmartFortwo();
        }

        //Refatorar
        private void VerifyCrewMembersMovement(IEnumerable<CrewMember> crewMembers)
        {
            HashSet<CrewMember> crewMembersBase = new HashSet<CrewMember>();
            foreach (var crewMember in crewMembers)
            {
                crewMembersBase.Add(crewMember);
            }

            foreach (var crewMember in crewMembers)
            {
                var crewMembersToCompare = new HashSet<CrewMember>();
                foreach (var member in crewMembersBase)
                {
                    crewMembersToCompare.Add(member);
                }

                if (crewMember is Prisoner)
                {
                    bool hasPoliceman = false;
                    foreach (var crewMemberAtPlace in crewMembers)
                    {
                        if (crewMemberAtPlace is Policeman)
                        {
                            hasPoliceman = true;
                            break;
                        }
                    }
                    if (!hasPoliceman)
                        throw new ArgumentException("The prisoner can't stay with the crew members without a policeman");
                }

                List<CrewMember> crewMembersThatCannotBeTogether = new List<CrewMember>();
                foreach (var crewMemberAtPlace in crewMembers)
                {
                    if (crewMember.CantStayAloneWith.Contains(crewMemberAtPlace.GetType()))
                        crewMembersThatCannotBeTogether.Add(crewMemberAtPlace);
                }

                crewMembersToCompare.IntersectWith(crewMembersThatCannotBeTogether);
                if (crewMembersThatCannotBeTogether.Count > 0 && crewMembersToCompare.Count == 0)
                    throw new ArgumentException("There is some crewMember who is accompanied by someone who should not");
            }
        }
    }
}
