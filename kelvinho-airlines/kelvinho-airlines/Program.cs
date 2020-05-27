using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines
{
    class Program
    {
        static void Main(string[] args)
        {
            var _smartFotwoService = new SmartFortwoService();

            List<CrewMember> crewMembers = new List<CrewMember>();
            crewMembers.Add(new Pilot("Soler"));
            crewMembers.Add(new Officer("Coleta"));
            crewMembers.Add(new Officer("Ivan"));
            crewMembers.Add(new FlightServiceChief("Kelvin"));
            crewMembers.Add(new FlightAttendant("Pâmela"));
            crewMembers.Add(new FlightAttendant("Nadia"));
            crewMembers.Add(new Policeman("Tyler"));
            crewMembers.Add(new Prisoner("Mankalão"));

            var terminal = Terminal.StartWithASmartFortwo(crewMembers);
            var airplane = new Airplane();

            Console.WriteLine("Started\n");
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            Console.WriteLine("Boarding\n");
            _smartFotwoService.Board(terminal, crewMembers[0], crewMembers[1]);
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            _smartFotwoService.Move(terminal, airplane);
            Console.WriteLine("moving :)");
            Console.WriteLine("______________________________________");

            Console.WriteLine("Disembarking");
            _smartFotwoService.DisembarkPassenger(airplane);
            Console.WriteLine($"\n{airplane.SmartFortwo}");
            Console.WriteLine($"\n{terminal}");
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            _smartFotwoService.Move(airplane, terminal);
            Console.WriteLine("moving :)");
            Console.WriteLine("______________________________________");

            Console.WriteLine("Disembarking");
            _smartFotwoService.DisembarkDriver(terminal);
            Console.WriteLine($"\n{terminal.SmartFortwo}");
            Console.WriteLine($"\n{terminal}");
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            Console.WriteLine("Boarding\n");
            _smartFotwoService.Board(terminal, crewMembers[4], crewMembers[7]);
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            _smartFotwoService.Move(terminal, airplane);
            Console.WriteLine("moving :)");
            Console.WriteLine("______________________________________");
        }
    }
}
