using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Services;
using kelvinho_airlines.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Services
{
    public class SmartFortwoServiceTests
    {
        private readonly ISmartFortwoService _smartFortwoService;

        public SmartFortwoServiceTests()
        {
            _smartFortwoService = new SmartFortwoService(
                new List<Type> { typeof(Pilot), typeof(FlightServiceChief), typeof(Policeman)}
             );
        }

        [Theory]
        [MemberData(nameof(IncompatibleCrewMembers))]
        public void should_return_exception_verifying_crew_members(CrewMember crewMember1, CrewMember crewMember2, string exception)
        {
            var terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember> { crewMember1, crewMember2 });
            
            _smartFortwoService.Board(
                terminal,
                terminal.CrewMembers.First(),
                terminal.CrewMembers.Last()
            );

            var result = Assert.Throws<Exception>(() => _smartFortwoService.Move(terminal, new Airplane()));

            Assert.Equal(result.Message, exception);
        }

        [Fact]
        public void should_move_smart_fortwo()
        {
            var terminal = Terminal.StartWithASmartFortwo(new HashSet<CrewMember> { new Pilot("Pilot"), new FlightServiceChief("FlightServiceChief") });

            _smartFortwoService.Board(
                terminal,
                terminal.CrewMembers.First(),
                terminal.CrewMembers.Last()
            );

            var airplane = new Airplane();

            _smartFortwoService.Move(terminal, airplane);

            Assert.NotNull(airplane.SmartFortwo);
            Assert.Null(terminal.SmartFortwo);
        }

        public static IEnumerable<object[]> IncompatibleCrewMembers()
        {
            yield return new object[] 
            {
                new Pilot("Pilot"),
                new FlightAttendant("Attendant"),
                "There is some crew members that cannot be together at the place"
            };

            yield return new object[] 
            { 
                new FlightServiceChief("FlightServiceChief"),
                new Officer("Officer"),
                "There is some crew members that cannot be together at the place"
            };

            yield return new object[]
            {
                new Pilot("Pilot"),
                new Prisoner("Prisoner"),
                "The prisoner can't stay with the others crew members without a policeman"
            };
        }
    }
}