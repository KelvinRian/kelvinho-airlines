using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class MovementServiceTests
    {
        private readonly ITripInformerService _tripInformerService;
        private readonly IMovementService _movementService;

        public MovementServiceTests()
        {
            _tripInformerService = Substitute.For<ITripInformerService>();
            var drivers = new List<Type>()
            {
                typeof(Pilot),
                typeof(Policeman),
                typeof(FlightServiceChief)
            };

            _movementService = new MovementService(drivers, _tripInformerService);
        }

        [Fact]
        public void Should_not_put_in_smart_fortwo_when_current_place_has_no_smart_forwto()
        {
            var currentPlace = new Airplane();
            var destinyPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.PutInTheSmartFortwo(null, null, currentPlace, destinyPlace));

            var expectedMessage = "The smart fortwo was not found!";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_not_put_in_smart_fortwo_when_driver_has_no_authorization()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var driver = new Officer("name");

            var excpetion = Assert.Throws<Exception>(() => _movementService.PutInTheSmartFortwo(driver, null, currentPlace, destinyPlace));

            var expectedMessage = $"{driver.Name} is not authorized to drive this vehicle";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_not_put_in_smart_fortwo_when_both_are_null()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            _movementService.PutInTheSmartFortwo(null, null, currentPlace, destinyPlace);

            Assert.Empty(currentPlace.CrewMembers);
            Assert.Null(currentPlace.SmartFortwo.Driver);

            _tripInformerService.DidNotReceive().ShowBoardingInfo(Arg.Any<CrewMember>(), Arg.Any<CrewMember>());
            _tripInformerService.DidNotReceive().ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_put_only_driver_in_smart_fortwo()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var driver = new Pilot("name");
            currentPlace.Board(driver);

            _movementService.PutInTheSmartFortwo(driver, null, currentPlace, destinyPlace);

            Assert.DoesNotContain(driver, currentPlace.CrewMembers);
            Assert.Equal(driver, currentPlace.SmartFortwo.Driver);

            _tripInformerService.Received(1).ShowBoardingInfo(driver, null);
            _tripInformerService.Received(1).ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_put_only_passenger_in_smart_fortwo()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var passenger = new Pilot("name");
            currentPlace.Board(passenger);

            _movementService.PutInTheSmartFortwo(null, passenger, currentPlace, destinyPlace);

            Assert.DoesNotContain(passenger, currentPlace.CrewMembers);
            Assert.Equal(passenger, currentPlace.SmartFortwo.Passenger);

            _tripInformerService.Received(1).ShowBoardingInfo(null, passenger);
            _tripInformerService.Received(1).ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_put_both_in_smart_fortwo()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var passenger = new Pilot("name");
            var driver = new Pilot("name");
            currentPlace.Board(passenger);
            currentPlace.Board(driver);

            _movementService.PutInTheSmartFortwo(driver, passenger, currentPlace, destinyPlace);

            Assert.DoesNotContain(passenger, currentPlace.CrewMembers);
            Assert.DoesNotContain(driver, currentPlace.CrewMembers);
            Assert.Equal(passenger, currentPlace.SmartFortwo.Passenger);
            Assert.Equal(driver, currentPlace.SmartFortwo.Driver);

            _tripInformerService.Received(1).ShowBoardingInfo(driver, passenger);
            _tripInformerService.Received(1).ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_not_move_when_current_place_does_not_have_a_smart_fortwo()
        {
            var currentPlace = new Airplane();
            var destinyPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.Move(currentPlace, destinyPlace));

            var expectedMessage = "The smart fortwo was not found!";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_not_move_when_smart_fortwo_does_not_have_a_driver()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.Move(currentPlace, destinyPlace));

            var expectedMessage = "Smart Fortwo can't move without a driver";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_not_move_when_incompatible_crew_membes_are_together_alone_in_current_place()
        {
            var destinyPlace = new Airplane();
            var crewMembers = new List<CrewMember> { new Prisoner("prisoner"), new Pilot("pilot") };
            var currentPlace = Terminal.CreateWithSmartFortwo(crewMembers);
            currentPlace.SmartFortwo.EnterDriver(new Pilot("pilot"));

            var excpetion = Assert.Throws<Exception>(() => _movementService.Move(currentPlace, destinyPlace));

            var expectedMessage = "Some incompatible crew members are together alone or the prisoner is far from policeman";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_not_move_when_incompatible_crew_membes_are_together_alone_in_smart_fortwo()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            currentPlace.SmartFortwo.EnterBoth(new Pilot("pilot"), new Prisoner("prisoner"));

            var excpetion = Assert.Throws<Exception>(() => _movementService.Move(currentPlace, destinyPlace));

            var expectedMessage = "Some incompatible crew members are together alone or the prisoner is far from policeman";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_move()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            currentPlace.SmartFortwo.EnterDriver(new Pilot("pilot"));

            var result = _movementService.Move(currentPlace, destinyPlace);

            Assert.Equal(destinyPlace, result.CurrentPlace);
            Assert.Equal(currentPlace, result.DestinyPlace);
        }

        [Fact]
        public void Should_not_disembark_passenger_when_current_place_has_no_smart_forwto()
        {
            var currentPlace = new Airplane();
            var destinyPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.DisembarkPassenger(currentPlace, destinyPlace));

            var expectedMessage = "The smart fortwo was not found!";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_disembark_passenger()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var passenger = new Pilot("pilot");
            var driver = new Pilot("otherPilot");
            currentPlace.SmartFortwo.EnterBoth(driver, passenger);

            _movementService.DisembarkPassenger(currentPlace, destinyPlace);

            Assert.Null(currentPlace.SmartFortwo.Passenger);
            Assert.Contains(passenger, currentPlace.CrewMembers);

            _tripInformerService.Received().ShowDisembarkingInfo(Arg.Any<List<CrewMember>>());
            _tripInformerService.Received().ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_not_disembark_driver_when_current_place_has_no_smart_forwto()
        {
            var currentPlace = new Airplane();
            var destinyPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.DisembarkDriver(currentPlace, destinyPlace));

            var expectedMessage = "The smart fortwo was not found!";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_disembark_driver()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var driver = new Pilot("pilot");
            currentPlace.SmartFortwo.EnterDriver(driver);

            _movementService.DisembarkDriver(currentPlace, destinyPlace);

            Assert.Null(currentPlace.SmartFortwo.Driver);
            Assert.Contains(driver, currentPlace.CrewMembers);

            _tripInformerService.Received().ShowDisembarkingInfo(Arg.Any<List<CrewMember>>());
            _tripInformerService.Received().ShowTripStateInfo(currentPlace, destinyPlace);
        }

        [Fact]
        public void Should_not_disembark_all_when_current_place_has_no_smart_forwto()
        {
            var currentPlace = new Airplane();
            var destinyPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var excpetion = Assert.Throws<Exception>(() => _movementService.DisembarkAll(currentPlace, destinyPlace));

            var expectedMessage = "The smart fortwo was not found!";

            Assert.Equal(expectedMessage, excpetion.Message);
        }

        [Fact]
        public void Should_disembark_all()
        {
            var destinyPlace = new Airplane();
            var currentPlace = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var passenger = new Pilot("pilot");
            var driver = new Pilot("otherPilot");
            currentPlace.SmartFortwo.EnterBoth(driver, passenger);

            _movementService.DisembarkAll(currentPlace, destinyPlace);

            Assert.Null(currentPlace.SmartFortwo.Passenger);
            Assert.Null(currentPlace.SmartFortwo.Driver);
            Assert.Contains(passenger, currentPlace.CrewMembers);
            Assert.Contains(driver, currentPlace.CrewMembers);

            _tripInformerService.Received().ShowDisembarkingInfo(Arg.Any<List<CrewMember>>());
            _tripInformerService.Received().ShowTripStateInfo(currentPlace, destinyPlace);
        }
    }
}
