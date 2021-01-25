using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class TripServiceTests
    {
        [Fact]
        public void should_execute_without_throw_exception()
        {
            var drivers = new List<Type>()
            {
                typeof(Pilot),
                typeof(Policeman),
                typeof(FlightServiceChief)
            };

            ITripInformerService tripInformerService = new TripInformerService();
            IMovementService movementService = new MovementService(drivers, tripInformerService);

            ITripService tripService = new TripService(tripInformerService, movementService);

            tripService.Execute();
        }
    }
}