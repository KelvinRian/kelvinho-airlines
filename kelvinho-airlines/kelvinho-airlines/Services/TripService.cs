using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services.Interfaces;
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
        private readonly Terminal _terminal;
        private readonly Airplane _airplane;
        private readonly List<Type> _drivers;
        private readonly string _dividingLine = "*******************************************************************************************";

        public TripService(List<Type> drivers)
        {
            _drivers = drivers;
            _terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember>
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
            _airplane = new Airplane();
            _currentPlace = _terminal;
        }

        public void Execute()
        {
            Console.WriteLine("Started\n");
            ShowInfo();
            GetInTheSmartFortwo(typeof(Pilot), typeof(Officer));
            Move();
            DisembarkPassenger();
            Move();
            GetInTheSmartFortwo(null, typeof(Officer));
            Move();
            DisembarkPassenger();
            Move();
            DisembarkDriver();
            GetInTheSmartFortwo(typeof(FlightServiceChief), typeof(FlightAttendant));
            Move();
            DisembarkPassenger();
            Move();
            GetInTheSmartFortwo(null, typeof(FlightAttendant));
            Move();
            DisembarkPassenger();
            Move();
            GetInTheSmartFortwo(null, typeof(Pilot));
            Move();
            DisembarkPassenger();
            Move();
            DisembarkDriver();
            GetInTheSmartFortwo(typeof(Policeman), typeof(Prisoner));
            Move();
            Disembark();
            GetInTheSmartFortwo(typeof(Pilot), null);
            Move();
            GetInTheSmartFortwo(null, typeof(FlightServiceChief));
            Move();
            Disembark();
        }

        private void ShowInfo()
        {
            var defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Location: {_currentPlace.GetType().Name}\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{_currentPlace.SmartFortwo}\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_terminal);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_airplane);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine(_dividingLine);
        }

        private void GetInTheSmartFortwo(Type driverType, Type passengerType)
        {
            if (_currentPlace.SmartFortwo == null)
                throw new Exception("The smart fortwo was not found!");

            CrewMember driver;
            CrewMember passenger;

            driver = _currentPlace.CrewMembers.FirstOrDefault(c => c.GetType() == driverType);
            passenger = _currentPlace.CrewMembers.FirstOrDefault(c => c.GetType() == passengerType);

            if (driver != null)
            {
                if (!_drivers.Contains(driver.GetType()))
                    throw new Exception($"{driver.Name} is not authorized to drive this vehicle");
            }

            _currentPlace.Disembark(new List<CrewMember>() { driver, passenger });

            if (passenger == null && driver == null)
            {
                return;
            }
            else if (passenger == null && driver != null)
            {
                _currentPlace.SmartFortwo.EnterDriver(driver);
            }
            else if (passenger != null && driver == null)
            {
                _currentPlace.SmartFortwo.EnterPassenger(passenger);
            }
            else
            {
                _currentPlace.SmartFortwo.EnterBoth(driver, passenger);
            }

            var crewMembers = new List<CrewMember> { driver, passenger };
            StringBuilder crewMembersInfo = new StringBuilder();

            foreach (var crewMember in crewMembers.Where(c => c != null))
            {
                if (crewMembersInfo.Length > 0)
                    crewMembersInfo.Append(", ");

                crewMembersInfo.Append(crewMember);
            }

            Console.WriteLine($"Boarding ({crewMembersInfo})\n");
            ShowInfo();
        }

        private void Move()
        {
            ShowMovementInfo();

            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            if (!SmartFortwoAtCurrentPlaceHasDriver())
                throw new Exception("Smart Fortwo can't move without a driver");

            VerifyCrewMembersMovement(_currentPlace.CrewMembers);
            VerifyCrewMembersMovement(_currentPlace.GetSmartFortwoCrewMembers());

            ChangePlaceOfSmartFortwo();

            Console.WriteLine($"\n{_dividingLine}");
        }

        private void ShowMovementInfo()
        {
            var origin = _currentPlace.GetType().Name;
            var destiny = _currentPlace is Terminal
                ? typeof(Airplane).Name
                : typeof(Terminal).Name;

            Console.WriteLine($"Moving ({origin} => {destiny})");
        }

        private bool CurrentPlaceHasSmartFortwo()
            => !_currentPlace.SmartFortwo.IsNull();

        private bool SmartFortwoAtCurrentPlaceHasDriver()
            => _currentPlace.SmartFortwoHasDriver();

        private void VerifyCrewMembersMovement(IEnumerable<CrewMember> crewMembers)
        {
            HashSet<Type> IncompatibleTypesOfCrewMembersAtPlace = new HashSet<Type>();
            HashSet<Type> crewMemberTypesAtPlace = new HashSet<Type>();
            bool hasPoliceman = false;
            bool hasPrisoner = false;

            foreach (var crewMember in crewMembers.Where(x => !x.IsNull()))
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

        private void ChangePlaceOfSmartFortwo()
        {
            if (_currentPlace is Terminal)
            {
                _airplane.SetSmartFortwo(_currentPlace.SmartFortwo);
                _currentPlace.RemoveSmartFortwo();
                _currentPlace = _airplane;
            }
            else
            {
                _terminal.SetSmartFortwo(_currentPlace.SmartFortwo);
                _currentPlace.RemoveSmartFortwo();
                _currentPlace = _terminal;
            }
        }

        private void DisembarkPassenger()
        {
            CrewMember passenger;

            if(!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            passenger = _currentPlace.DisembarkSmartFortwoPassenger();
            _currentPlace.Board(passenger);

            Console.WriteLine($"Disembarking ({passenger})\n");
            ShowInfo();
        }

        private void DisembarkDriver()
        {
            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            var driver = _currentPlace.DisembarkSmartFortwoDriver();
            _currentPlace.Board(driver);

            Console.WriteLine($"Disembarking ({driver})\n");
            ShowInfo();
        }

        private void Disembark()
        {
            if (!CurrentPlaceHasSmartFortwo())
                throw new Exception("The smart fortwo was not found!");

            var crewMembers = _currentPlace.DisembarkAllFromSmartFortwo();
            _currentPlace.Board(crewMembers);

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