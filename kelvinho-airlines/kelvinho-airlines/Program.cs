using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
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
            var smartFotwoService = new SmartFortwoService(drivers);

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

            ITripService tripService = new TripService(smartFotwoService, terminal, airplane);

            tripService.Execute();
        }
    }
}