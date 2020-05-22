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
            var _tripService = new TripService();

            List<CrewMember> crewMembers = new List<CrewMember>();
            crewMembers.Add(new Pilot("Soler"));
            crewMembers.Add(new Officer("Coleta"));
            crewMembers.Add(new Officer("Ivan"));
            crewMembers.Add(new FlightServiceChief("Kelvin"));
            crewMembers.Add(new FlightAttendant("Pâmela"));
            crewMembers.Add(new FlightAttendant("Nadia"));
            crewMembers.Add(new Policeman("Tyler"));
            crewMembers.Add(new Prisoner("Mankalão"));

            var terminal = new Terminal(crewMembers, new SmartFortwo());
            var airplane = new Airplane();

            Console.WriteLine("Started\n");
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            Console.WriteLine("Boarding\n");
            _tripService.BoardTheSmartFortwo(terminal, crewMembers[0], crewMembers[1]);
            Console.WriteLine($"{terminal.SmartFortwo}\n");
            Console.WriteLine(terminal);
            Console.WriteLine(airplane);
            Console.WriteLine("______________________________________");

            _tripService.Move(terminal, airplane);
            Console.WriteLine("moving :)");
            Console.WriteLine("______________________________________");

            Console.WriteLine("Disembarking");
            _tripService.DisembarkPassengerFromSmartFortwo(airplane);
            Console.WriteLine($"\n{airplane.SmartFortwo}");
            Console.WriteLine($"\n{terminal}");
            Console.WriteLine(airplane);
        }
    }
}
