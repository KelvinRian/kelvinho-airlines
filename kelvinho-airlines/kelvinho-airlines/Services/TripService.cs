using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services.Interfaces;
using kelvinho_airlines.Utils;
using kelvinho_airlines.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kelvinho_airlines.Services
{
    public class TripService : ITripService
    {
        private Place _currentPlace;
        private Place _destinyPlace;
        private ITripInformerService _tripInformerService;
        private IMovementService _movementService;

        public TripService(ITripInformerService tripInformerService, IMovementService movementService)
        {
            _currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>
            {
                new Pilot("Soler"),
                new Officer("Coleta"),
                new Officer("Ivan"),
                new FlightServiceChief("Kelvin"),
                new FlightAttendant("Pâmela"),
                new FlightAttendant("Guerreiro"),
                new Policeman("Tyler"),
                new Prisoner("Mahnke")
            });
            _destinyPlace = new Airplane();

            _tripInformerService = tripInformerService;
            _movementService = movementService;
        }

        public void Execute()
        {
            _tripInformerService.ShowStartMessage();

            _tripInformerService.ShowTripStateInfo(_currentPlace, _destinyPlace);

            _movementService.PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot), _currentPlace.CrewMembers.FirstOrDefault(x => x is Officer), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkPassenger(_currentPlace, _destinyPlace);

            Move();

            _movementService.PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is Officer), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkPassenger(_currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkDriver(_currentPlace, _destinyPlace);

            _movementService.PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is FlightServiceChief), _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightAttendant), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkPassenger(_currentPlace, _destinyPlace);

            Move();

            _movementService.PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightAttendant), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkPassenger(_currentPlace, _destinyPlace);

            Move();

            _movementService.PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkPassenger(_currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkDriver(_currentPlace, _destinyPlace);

            _movementService.PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Policeman), _currentPlace.CrewMembers.FirstOrDefault(x => x is Prisoner), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkAll(_currentPlace, _destinyPlace);

            _movementService.PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot), null, _currentPlace, _destinyPlace);

            Move();

            _movementService.PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightServiceChief), _currentPlace, _destinyPlace);

            Move();

            _movementService.DisembarkAll(_currentPlace, _destinyPlace);
        }

        private void Move()
        {
            var tripDto = _movementService.Move(_currentPlace, _destinyPlace);
            _currentPlace = tripDto.CurrentPlace;
            _destinyPlace = tripDto.DestinyPlace;
        }
    }
}