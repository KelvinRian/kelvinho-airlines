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

        public TripService(
            ISmartFortwoService smartFortwoService,
            Terminal terminal,
            Airplane airplane)
        {
            _smartFortwoService = smartFortwoService;
            _terminal = terminal;
            _airplane = airplane;
        }

        public void Execute()
        {
            Console.WriteLine("Started\n");
            ShowInfo();
            Board(typeof(Pilot), typeof(Officer));
            ShowInfo();
            Move();
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

        private void Board(Type driver, Type passenger)
        {
            var crewMembers = new List<CrewMember>();

            if (_terminal.SmartFortwo != null)
            {
                crewMembers = _smartFortwoService.Board(
                    _terminal,
                    _terminal.CrewMembers.FirstOrDefault(c => c.GetType() == driver),
                    _terminal.CrewMembers.FirstOrDefault(c => c.GetType() == passenger));
            }
            else if (_airplane.SmartFortwo != null)
            {
                crewMembers = _smartFortwoService.Board(
                    _airplane,
                    _airplane.CrewMembers.FirstOrDefault(c => c.GetType() == driver),
                    _airplane.CrewMembers.FirstOrDefault(c => c.GetType() == passenger));
            }
            else
            {
                throw new ArgumentException("The smart fortwo was not found!");
            }

            StringBuilder crewMembersBoarding = new StringBuilder();

            foreach (var crewMember in crewMembers)
            {
                if (crewMembersBoarding.Length > 0)
                    crewMembersBoarding.Append(", ");

                if (crewMember != null)
                    crewMembersBoarding.Append(crewMember);
            }

            Console.WriteLine($"Boarding ({crewMembersBoarding})\n");
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
                throw new ArgumentException("The smart fortwo was not found!");
            }

            Console.WriteLine("\n*******************************************************************************************");
        }
    }
}