using kelvinho_airlines.Dtos;
using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Services.Interfaces;
using kelvinho_airlines.Utils;
using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kelvinho_airlines.Services
{
    public class MovementService : IMovementService
    {
        private readonly List<Type> _drivers;
        private readonly ITripInformerService _tripInformerService;

        public MovementService(List<Type> drivers, ITripInformerService tripInformerService)
        {
            _drivers = drivers;
            _tripInformerService = tripInformerService;
        }

        public TripDto Move(Place currentPlace, Place destinyPlace)
        {
            _tripInformerService.ShowMovementInfo(currentPlace, destinyPlace);

            if (!CurrentPlaceHasSmartFortwo(currentPlace))
                throw new Exception("The smart fortwo was not found!");

            if (!SmartFortwoAtCurrentPlaceHasDriver(currentPlace))
                throw new Exception("Smart Fortwo can't move without a driver");

            var currentPlaceMembersCanStayTogether = CrewChecker.CrewMembersAreAllowedToStayTogether(currentPlace.CrewMembers);
            var smartFortwoMembersCanStayTogether = CrewChecker.CrewMembersAreAllowedToStayTogether(currentPlace.GetSmartFortwoCrewMembers());

            if (!currentPlaceMembersCanStayTogether || !smartFortwoMembersCanStayTogether)
                throw new Exception("Some incompatible crew members are together alone or the prisoner is far from policeman");

            return ChangePlaceOfSmartFortwo(currentPlace, destinyPlace);
        }

        private bool CurrentPlaceHasSmartFortwo(Place place)
            => !place.SmartFortwo.IsNull();

        private bool SmartFortwoAtCurrentPlaceHasDriver(Place place)
            => place.SmartFortwoHasDriver();

        private TripDto ChangePlaceOfSmartFortwo(Place currentPlace, Place destinyPlace)
        {
            destinyPlace.SetSmartFortwo(currentPlace.SmartFortwo);
            currentPlace.RemoveSmartFortwo();

            var newCurrentPlace = destinyPlace;
            destinyPlace = currentPlace;
            currentPlace = newCurrentPlace;
            return new TripDto(currentPlace, destinyPlace);
        }

        public void PutInTheSmartFortwo(CrewMember driver, CrewMember passenger, Place currentPlace, Place destinyPlace)
        {
            if (!CurrentPlaceHasSmartFortwo(currentPlace))
                throw new Exception("The smart fortwo was not found!");

            if (!driver.IsNull())
                if (!DriverHasAuthorization(driver))
                    throw new Exception($"{driver.Name} is not authorized to drive this vehicle");

            currentPlace.Remove(driver, passenger);

            var shouldPutBoth = !passenger.IsNull() && !driver.IsNull();
            var shouldPutOnlyDriver = !driver.IsNull() && passenger.IsNull();
            var shouldPutOnlyPassenger = !passenger.IsNull() && driver.IsNull();

            if (!shouldPutBoth && !shouldPutOnlyDriver && !shouldPutOnlyPassenger)
                return;

            else if (shouldPutOnlyDriver)
                currentPlace.PutDriverInSmartFortwo(driver);

            else if (shouldPutOnlyPassenger)
                currentPlace.PutPassengerInSmartFortwo(passenger);

            else
                currentPlace.PutBothInSmartFortwo(driver, passenger);

            _tripInformerService.ShowBoardingInfo(driver, passenger);
            _tripInformerService.ShowTripStateInfo(currentPlace, destinyPlace);
        }

        private bool DriverHasAuthorization(CrewMember driver)
            => _drivers.Contains(driver.GetType());

        public void DisembarkDriver(Place currentPlace, Place destinyPlace)
        {
            if (!CurrentPlaceHasSmartFortwo(currentPlace))
                throw new Exception("The smart fortwo was not found!");

            var driver = currentPlace.DisembarkDriverFromSmartFortwo();
            currentPlace.Board(driver);

            _tripInformerService.ShowDisembarkingInfo(new List<CrewMember> { driver });

            _tripInformerService.ShowTripStateInfo(currentPlace, destinyPlace);
        }

        public void DisembarkPassenger(Place currentPlace, Place destinyPlace)
        {
            CrewMember passenger;

            if (!CurrentPlaceHasSmartFortwo(currentPlace))
                throw new Exception("The smart fortwo was not found!");

            passenger = currentPlace.DisembarkPassengerFromSmartFortwo();
            currentPlace.Board(passenger);

            _tripInformerService.ShowDisembarkingInfo(new List<CrewMember> { passenger });

            _tripInformerService.ShowTripStateInfo(currentPlace, destinyPlace);
        }

        public void DisembarkAll(Place currentPlace, Place destinyPlace)
        {
            if (!CurrentPlaceHasSmartFortwo(currentPlace))
                throw new Exception("The smart fortwo was not found!");

            var crewMembers = currentPlace.DisembarkAllFromSmartFortwo();
            currentPlace.Board(crewMembers.ToList());

            _tripInformerService.ShowDisembarkingInfo(crewMembers);

            _tripInformerService.ShowTripStateInfo(currentPlace, destinyPlace);
        }
    }
}
