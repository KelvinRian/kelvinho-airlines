using kelvinho_airlines.Entities;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;

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

            ITripService tripService = new TripService(smartFotwoService);

            tripService.Execute();
        }
    }
}