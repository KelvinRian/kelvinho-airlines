using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines.Services
{
    public class SmartFortwoService
    {
        private readonly List<Type> _crewMembersAuthorizedToDrive;
        private Place _currentPlace;
        private Place _destinyPlace;
        private CrewMember _passenger;
        private Place _driver;

        public SmartFortwoService(List<Type> crewMembersAuthorizedToDrive, Place currentPlace, Place destinyPlace)
        {
            _crewMembersAuthorizedToDrive = crewMembersAuthorizedToDrive;
            _currentPlace = currentPlace;
            _destinyPlace = destinyPlace;
        }

        public void MoveToDestiny()
        {

        }

        private void ChangePlace()
        {

        }

        private void GetAsDriver(CrewMember driver)
        {

        }

        private void GetAsPassenger(CrewMember passenger)
        {

        }

        private void GetBoth(CrewMember driver, CrewMember passenger)
        {

        }

        private void PutDriver(CrewMember driver)
        {

        }

        private void PutPassenger(CrewMember passenger)
        {

        }

        private void PutBoth(CrewMember driver, CrewMember passenger)
        {

        }
    }
}
