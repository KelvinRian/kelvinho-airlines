﻿using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kelvinho_airlines.Services
{
    public class SmartFortwoService : ISmartFortwoService
    {
        public SmartFortwoService(List<Type> drivers)
        {
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
    }
}
