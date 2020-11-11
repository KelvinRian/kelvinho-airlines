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
        private readonly ISmartFortwoService _smartFortwoService;
        private readonly Terminal _terminal;
        private readonly Airplane _airplane;
        private readonly List<Type> _drivers;

        public TripService(ISmartFortwoService smartFortwoService, List<Type> drivers)
        {
            _drivers = drivers;
            _smartFortwoService = smartFortwoService;
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

                _terminal.SmartFortwo.GetIn(
                    _terminal,
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

                _airplane.SmartFortwo.GetIn(
                    _airplane,
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
            if (_terminal.SmartFortwo != null)
            {
                _smartFortwoService.Move(_terminal, _airplane);
                Console.WriteLine("Moving (Terminal => Airplane)");
            }
            else if (_airplane.SmartFortwo != null)
            {
                _smartFortwoService.Move(_airplane, _terminal);
                Console.WriteLine("Moving (Airplane => Terminal)");
            }
            else
            {
                throw new Exception("The smart fortwo was not found!");
            }

            Console.WriteLine("\n*******************************************************************************************");
        }

        private void DisembarkPassenger()
        {
            CrewMember passenger;

            if (_terminal.SmartFortwo != null)
            {
                passenger = _smartFortwoService.DisembarkPassenger(_terminal);
            }
            else if (_airplane.SmartFortwo != null)
            {
                passenger = _smartFortwoService.DisembarkPassenger(_airplane);
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
                driver = _smartFortwoService.DisembarkDriver(_terminal);
            }
            else if (_airplane.SmartFortwo != null)
            {
                driver = _smartFortwoService.DisembarkDriver(_airplane);
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
                crewMembers = _terminal.SmartFortwo.DisembarkAllIn(_terminal).ToList();
            }
            else if (_airplane.SmartFortwo != null)
            {
                crewMembers = _airplane.SmartFortwo.DisembarkAllIn(_airplane).ToList();
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