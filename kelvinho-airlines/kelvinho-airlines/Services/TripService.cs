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
        private readonly List<Type> _drivers;

        public TripService(List<Type> drivers)
        {
            _drivers = drivers;
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
        }

        public void Execute()
        {
            Console.WriteLine("Started\n");
            ShowInfo();
            PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot), _currentPlace.CrewMembers.FirstOrDefault(x => x is Officer));
            Move();
            DisembarkPassenger();
            Move();
            PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is Officer));
            Move();
            DisembarkPassenger();
            Move();
            DisembarkDriver();
            PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is FlightServiceChief), _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightAttendant));
            Move();
            DisembarkPassenger();
            Move();
            PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightAttendant));
            Move();
            DisembarkPassenger();
            Move();
            PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot));
            Move();
            DisembarkPassenger();
            Move();
            DisembarkDriver();
            PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Policeman), _currentPlace.CrewMembers.FirstOrDefault(x => x is Prisoner));
            Move();
            DisembarkAll();
            PutInTheSmartFortwo(_currentPlace.CrewMembers.FirstOrDefault(x => x is Pilot), null);
            Move();
            PutInTheSmartFortwo(null, _currentPlace.CrewMembers.FirstOrDefault(x => x is FlightServiceChief));
            Move();
            DisembarkAll();
        }

        private void ShowInfo()
        {
            var defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Location: {_currentPlace.GetType().Name}\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{_currentPlace.SmartFortwo}\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            var terminal = _currentPlace is Terminal ? _currentPlace : _destinyPlace;
            Console.WriteLine(terminal);

            Console.ForegroundColor = ConsoleColor.Red;
            var airplane = _currentPlace is Airplane ? _currentPlace : _destinyPlace;
            Console.WriteLine(airplane);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine(TextHelper.DividingLine);
        }

        private void PutInTheSmartFortwo(CrewMember driver, CrewMember passenger)
        {
            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            if (!driver.IsNull())
                if (!DriverHasAuthorization(driver))
                    throw new Exception($"{driver.Name} is not authorized to drive this vehicle");

            _currentPlace.Remove(driver, passenger);

            var shouldPutBoth = !passenger.IsNull() && !driver.IsNull();
            var shouldPutOnlyDriver = !driver.IsNull() && passenger.IsNull();
            var shouldPutOnlyPassenger = !passenger.IsNull() && driver.IsNull();

            if (!shouldPutBoth && !shouldPutOnlyDriver && !shouldPutOnlyPassenger)
                return;

            else if (shouldPutOnlyDriver)
                _currentPlace.PutDriverInSmartFortwo(driver);

            else if (shouldPutOnlyPassenger)
                _currentPlace.PutPassengerInSmartFortwo(passenger);

            else
                _currentPlace.PutBothInSmartFortwo(driver, passenger);

            ShowBoardingInfo(driver, passenger);
            ShowInfo();
        }

        private bool CurrentPlaceHasSmartFortwo()
            => !_currentPlace.SmartFortwo.IsNull();

        private bool DriverHasAuthorization(CrewMember driver)
            => _drivers.Contains(driver.GetType());

        private void ShowBoardingInfo(params CrewMember[] crewMembers)
        {
            StringBuilder crewMembersInfo = new StringBuilder();

            foreach (var crewMember in crewMembers.Where(c => !c.IsNull()))
            {
                if (crewMembersInfo.Length > 0)
                    crewMembersInfo.Append(", ");

                crewMembersInfo.Append(crewMember);
            }

            Console.WriteLine($"Boarding ({crewMembersInfo})\n");
        }

        private void Move()
        {
            ShowMovementInfo();

            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            if (!SmartFortwoAtCurrentPlaceHasDriver())
                throw new Exception("Smart Fortwo can't move without a driver");

            var currentPlaceMembersCanStayTogether = CrewChecker.CrewMembersAreAllowedToStayTogether(_currentPlace.CrewMembers);
            var smartFortwoMembersCanStayTogether = CrewChecker.CrewMembersAreAllowedToStayTogether(_currentPlace.GetSmartFortwoCrewMembers());

            if (!currentPlaceMembersCanStayTogether || !smartFortwoMembersCanStayTogether)
                throw new Exception("Some incompatible crew members are together alone or the prisoner is far from policeman");

            ChangePlaceOfSmartFortwo();

            Console.WriteLine($"\n{TextHelper.DividingLine}");
        }

        private void ShowMovementInfo()
        {
            var origin = _currentPlace.GetType().Name;
            var destiny = _destinyPlace.GetType().Name;

            Console.WriteLine($"Moving ({origin} => {destiny})");
        }

        private bool SmartFortwoAtCurrentPlaceHasDriver()
            => _currentPlace.SmartFortwoHasDriver();

        private void ChangePlaceOfSmartFortwo()
        {
            _destinyPlace.SetSmartFortwo(_currentPlace.SmartFortwo);
            _currentPlace.RemoveSmartFortwo();

            var newCurrentPlace = _destinyPlace;
            _destinyPlace = _currentPlace;
            _currentPlace = newCurrentPlace;
        }

        private void DisembarkPassenger()
        {
            CrewMember passenger;

            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            passenger = _currentPlace.DisembarkPassengerFromSmartFortwo();
            _currentPlace.Board(passenger);

            Console.WriteLine($"Disembarking ({passenger})\n");
            ShowInfo();
        }

        private void DisembarkDriver()
        {
            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            var driver = _currentPlace.DisembarkDriverFromSmartFortwo();
            _currentPlace.Board(driver);

            Console.WriteLine($"Disembarking ({driver})\n");
            ShowInfo();
        }

        private void DisembarkAll()
        {
            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            var crewMembers = _currentPlace.DisembarkAllFromSmartFortwo();
            _currentPlace.Board(crewMembers.ToList());

            ShowCrewMembersDisembarking(crewMembers);
            ShowInfo();
        }

        private void ShowCrewMembersDisembarking(IEnumerable<CrewMember> crewMembers)
        {
            StringBuilder crewMembersDisembarking = new StringBuilder();

            foreach (var crewMember in crewMembers)
            {
                if (crewMembersDisembarking.Length > 0)
                    crewMembersDisembarking.Append(", ");

                if (!crewMember.IsNull())
                    crewMembersDisembarking.Append(crewMember);
            }

            Console.WriteLine($"Disembarking ({crewMembersDisembarking})\n");
        }
    }
}