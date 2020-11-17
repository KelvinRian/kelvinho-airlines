using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services.Interfaces;
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

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (_terminal.SmartFortwo != null)
                Console.WriteLine($"{_terminal.SmartFortwo}\n");
            else if (_airplane.SmartFortwo != null)
                Console.WriteLine($"{_airplane.SmartFortwo}\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_terminal);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_airplane);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine("*******************************************************************************************");
        }

        private void GetInTheSmartFortwo(Type driverType, Type passengerType)
        {
            CrewMember driver;
            CrewMember passenger;

            if (_terminal.SmartFortwo != null)
            {
                driver = _terminal.CrewMembers.FirstOrDefault(c => c.GetType() == driverType);
                passenger = _terminal.CrewMembers.FirstOrDefault(c => c.GetType() == passengerType);

                if (driver != null)
                {
                    if (!_drivers.Contains(driver.GetType()))
                        throw new Exception($"{driver.Name} is not authorized to drive this vehicle");
                }

                _terminal.Disembark(new List<CrewMember>() { driver, passenger });
                _terminal.SmartFortwo.GetIn(
                    driver,
                    passenger);
            }
            else if (_airplane.SmartFortwo != null)
            {
                driver = _airplane.CrewMembers.FirstOrDefault(c => c.GetType() == driverType);
                passenger = _airplane.CrewMembers.FirstOrDefault(c => c.GetType() == passengerType);

                if (driver != null)
                {
                    if (!_drivers.Contains(driver.GetType()))
                        throw new Exception($"{driver.Name} is not authorized to drive this vehicle");
                }

                _airplane.Disembark(new List<CrewMember>() { driver, passenger });
                _airplane.SmartFortwo.GetIn(
                    driver,
                    passenger);
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
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
            if(_currentPlace is Terminal)
            {
                Console.WriteLine("Moving (Terminal => Airplane)");

                VerifyCrewMembersMovement(_terminal.CrewMembers);
                var crewMembersInSmartFortwo = new List<CrewMember> { _terminal.SmartFortwo.Driver };
                if (_terminal.SmartFortwo.Passenger != null)
                    crewMembersInSmartFortwo.Add(_terminal.SmartFortwo.Passenger);

                VerifyCrewMembersMovement(crewMembersInSmartFortwo);

                _currentPlace = _airplane;
                _currentPlace.SetSmartFortwo(_terminal.SmartFortwo);

                if (_currentPlace.SmartFortwo.Driver == null)
                    throw new Exception("Smart Fortwo can't move without a driver");

                _terminal.RemoveSmartFortwo();
            }
            else if(_currentPlace is Airplane)
            {
                Console.WriteLine("Moving (Airplane => Terminal)");

                VerifyCrewMembersMovement(_airplane.CrewMembers);
                var crewMembersInSmartFortwo = new List<CrewMember> { _airplane.SmartFortwo.Driver };
                if (_airplane.SmartFortwo.Passenger != null)
                    crewMembersInSmartFortwo.Add(_airplane.SmartFortwo.Passenger);

                VerifyCrewMembersMovement(crewMembersInSmartFortwo);

                _currentPlace = _terminal;
                _currentPlace.SetSmartFortwo(_airplane.SmartFortwo);

                if (_currentPlace.SmartFortwo.Driver == null)
                    throw new Exception("Smart Fortwo can't move without a driver");

                _airplane.RemoveSmartFortwo();
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
            }
            Console.WriteLine("\n*******************************************************************************************");
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

        private void DisembarkPassenger()
        {
            CrewMember passenger;

            if (_terminal.SmartFortwo != null)
            {
                passenger = _terminal.SmartFortwo.DisembarkPassenger();
                _terminal.Board(new HashSet<CrewMember> { passenger });
            }
            else if (_airplane.SmartFortwo != null)
            {
                passenger = _airplane.SmartFortwo.DisembarkPassenger();
                _airplane.Board(new HashSet<CrewMember> { passenger });
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
            }

            Console.WriteLine($"Disembarking ({passenger})\n");
            ShowInfo();
        }

        private void DisembarkDriver()
        {
            CrewMember driver;

            if (_terminal.SmartFortwo != null)
            {
                driver = _terminal.SmartFortwo.DisembarkDriver();
                _terminal.Board(new HashSet<CrewMember> { driver });
            }
            else if (_airplane.SmartFortwo != null)
            {
                driver = _airplane.SmartFortwo.DisembarkDriver();
                _airplane.Board(new HashSet<CrewMember> { driver });
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
            }

            Console.WriteLine($"Disembarking ({driver})\n");
            ShowInfo();
        }

        private void Disembark()
        {
            List<CrewMember> crewMembers;

            if (_terminal.SmartFortwo != null)
            {
                crewMembers = _terminal.SmartFortwo.DisembarkAll().ToList();
                _terminal.Board(crewMembers.ToHashSet());
            }
            else if (_airplane.SmartFortwo != null)
            {
                crewMembers = _airplane.SmartFortwo.DisembarkAll().ToList();
                _airplane.Board(crewMembers.ToHashSet());
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
            }

            StringBuilder crewMembersDisembarking = new StringBuilder();

            foreach (var crewMember in crewMembers)
            {
                if (crewMembersDisembarking.Length > 0)
                    crewMembersDisembarking.Append(", ");

                if (crewMember != null)
                    crewMembersDisembarking.Append(crewMember);
            }

            Console.WriteLine($"Disembarking ({crewMembersDisembarking})\n");
            ShowInfo();
        }
    }
}