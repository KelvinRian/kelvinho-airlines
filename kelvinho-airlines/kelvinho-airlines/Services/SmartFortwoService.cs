using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace kelvinho_airlines.Services
{
    public class SmartFortwoService : ISmartFortwoService
    {
        private readonly List<string> drivers;

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

            //Refatorar
            VerifyCrewMembersMovement(origin.CrewMembers);
            VerifyCrewMembersMovement(new List<CrewMember> { origin.SmartFortwo.Driver, origin.SmartFortwo.Passenger });

            destiny.SetSmartFortwo(origin.SmartFortwo);
            origin.RemoveSmartFortwo();
        }

        //Refatorar
        private void VerifyCrewMembersMovement(IEnumerable<CrewMember> crewMembers)
        {
            //Hashset que recebe uma cópia da lista passada por parâmetro para poder utilizar o método
            //intersctWith sem perder dados da lista principal
            HashSet<CrewMember> crewMembersToCapare = new HashSet<CrewMember>();
            foreach (var crewMember in crewMembers)
            {
                crewMembersToCapare.Add(crewMember);
            }

            //Para cada crewMember na lista, é verificado se existe um tipo de crewMember que não pode estar acompanhado
            //do crewMember em questâo sem a presença de outro crewMember, se existir, é armazenado o tipo desse crewmember em outra lista
            foreach (var crewMember in crewMembers)
            {
                //Como o prisioneiro nao pode ficar junto com os outros tripulantes sem a presença de um policial
                //é realizado esse foreach pra verificar se existe um policial quando o crewMember analisado é um prisioneiro
                if (crewMember is Prisoner)
                {
                    bool hasPoliceman = false;
                    bool hasMoreOneCrewMember = false;
                    foreach (var crewMemberAtPlace in crewMembers)
                    {
                        if (crewMemberAtPlace is Policeman)
                            hasPoliceman = true;

                        else if (!(crewMemberAtPlace is Prisoner))
                            hasMoreOneCrewMember = true;

                    }
                    if (!hasPoliceman && hasMoreOneCrewMember)
                        throw new ArgumentException("The prisoner can't stay with the crew members without a policeman");
                }

                List<CrewMember> crewMembersThatCannotBeTogether = new List<CrewMember>();
                foreach (var crewMemberAtPlace in crewMembers)
                {
                    if (crewMember.CantBeAloneWith.Contains(crewMemberAtPlace.GetType().Name))
                        crewMembersThatCannotBeTogether.Add(crewMemberAtPlace);
                }

                //após a chamada do método intersectWith, se restar algum crewmember significa que possuí outro tipo de crewmember no lugar
                //o que faz com que a movimentacao seja valida
                crewMembersToCapare.IntersectWith(crewMembersThatCannotBeTogether);
                if (crewMembersToCapare.Count == 0)
                    throw new ArgumentException("There is some crewMember who is accompanied by someone who should not");
            }
        }
    }
}
