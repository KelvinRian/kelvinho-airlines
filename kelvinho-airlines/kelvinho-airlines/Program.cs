using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using System;
using System.Collections.Generic;

namespace kelvinho_airlines
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CrewMember> crewMembers = new List<CrewMember>();
            crewMembers.Add(new Pilot("Soler"));
            crewMembers.Add(new Officer("Coleta"));
            crewMembers.Add(new Officer("Ivan"));
            crewMembers.Add(new FlightServiceChief("Kelvin"));
            crewMembers.Add(new FlightAttendant("Pâmela"));
            crewMembers.Add(new FlightAttendant("Nadia"));
            crewMembers.Add(new Policeman("Tyler"));
            crewMembers.Add(new Prisoner("Mankalão"));

            var terminal = new Terminal(crewMembers);

            Console.WriteLine(terminal);
        }
    }
}
