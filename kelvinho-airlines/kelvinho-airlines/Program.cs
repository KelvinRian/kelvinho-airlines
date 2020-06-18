using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kelvinho_airlines
{
    class Program
    {
        static void Main(string[] args)
        {
            var drivers = new List<Type>()
            {
                typeof(Pilot),
                typeof(Policeman),
                typeof(FlightServiceChief)
            };
            var _smartFotwoService = new SmartFortwoService(drivers);

            var terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember>
            {
                new Pilot("Soler"),
                new Officer("Coleta"),
                new Officer("Ivan"),
                new FlightServiceChief("Kelvin"),
                new FlightAttendant("Pâmela"),
                new FlightAttendant("Guerreiro"),
                new Policeman("Tyler"),
                new Prisoner("Mankalão")
            });
            var airplane = new Airplane();

            Console.WriteLine("Started\n");
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("____________________________________________________________________________");

            Console.WriteLine("Boarding\n");
            _smartFotwoService.Board(terminal, terminal.CrewMembers.First(c => c is Pilot), terminal.CrewMembers.First(c => c is Officer));
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("____________________________________________________________________________");

            Console.WriteLine("Moving :)\n");
            _smartFotwoService.Move(terminal, airplane);
            Console.WriteLine("____________________________________________________________________________");

            Console.WriteLine("Disembarking");
            _smartFotwoService.DisembarkPassenger(airplane);
            Console.WriteLine($"\n{airplane.SmartFortwo}");
            Console.WriteLine($"\n{terminal}");
            Console.WriteLine(airplane);
            Console.WriteLine("____________________________________________________________________________");

            Console.WriteLine("Moving :)\n");
            _smartFotwoService.Move(airplane, terminal);
            Console.WriteLine("____________________________________________________________________________");

            Console.WriteLine("Disembarking");
            _smartFotwoService.DisembarkDriver(terminal);
            Console.WriteLine($"\n{terminal.SmartFortwo}");
            Console.WriteLine($"\n{terminal}");
            Console.WriteLine(airplane);
            Console.WriteLine("____________________________________________________________________________");
        }
    }
}
