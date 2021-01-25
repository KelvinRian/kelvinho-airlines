using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Tests.Services
{
    public class TripInformerServiceTests
    {
        private readonly ITripInformerService _service;

        public TripInformerServiceTests()
        {
            _service = new TripInformerService();
        }

        [Fact]
        public void Should_show_movement_info()
        {
            var airplane = new Airplane();
            var terminal = Terminal.CreateWithSmartFortwo(new List<CrewMember>());

            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowMovementInfo(terminal, airplane);

            var expectedOutput = "Moving (Terminal => Airplane)\r\n\n*******************************************************************************************\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void Should_show_state_info_with_terminal_as_current_place()
        {
            var terminal = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var airplane = new Airplane();

            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowTripStateInfo(terminal, airplane);

            var expectedOutput = "Location: Terminal\n\r\nSmart Fortwo:   |   Driver:  Empty   |   Passenger:  Empty\n\r\nTerminal: Empty\n\r\nAirplane:\n\nTechnical Crew:   \n\nCabin Crew:   \n\nCommon Crew:   \n\r\n*******************************************************************************************\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void Should_show_state_info_with_airplane_as_current_place()
        {
            var terminal = Terminal.CreateWithSmartFortwo(new List<CrewMember>());
            var airplane = new Airplane();
            
            airplane.SetSmartFortwo(terminal.SmartFortwo);
            terminal.RemoveSmartFortwo();

            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowTripStateInfo(airplane, terminal);

            var expectedOutput = "Location: Airplane\n\r\nSmart Fortwo:   |   Driver:  Empty   |   Passenger:  Empty\n\r\nTerminal: Empty\n\r\nAirplane:\n\nTechnical Crew:   \n\nCabin Crew:   \n\nCommon Crew:   \n\r\n*******************************************************************************************\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void Should_show_boarding_info()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowBoardingInfo(new Pilot("Pilot"), new Officer("Officer"));

            var expectedOutput = "Boarding (Pilot: Pilot, Officer: Officer)\n\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void Should_show_start_message()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowStartMessage();

            var expectedOutput = "Started\n\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void Show_should_disembarking_info()
        {
            var crewMembers = new List<CrewMember>
            {
                new Pilot("Pilot"),
                null,
                new Officer("Officer")
            };

            var output = new StringWriter();
            Console.SetOut(output);

            _service.ShowDisembarkingInfo(crewMembers);

            var expectedOutput = "Disembarking (Pilot: Pilot, , Officer: Officer)\n\r\n";

            Assert.Equal(expectedOutput, output.ToString());
        }
    }
}
