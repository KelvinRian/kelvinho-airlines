using kelvinho_airlines.Entities.CrewMembers;
using kelvinho_airlines.Utils;
using System.Collections.Generic;
using Xunit;

namespace Tests.Utils
{
    public class CrewCheckerTests
    {
        [Fact]
        public void Should_return_false_when_two_incompatible_crew_members_are_alone()
        {
            var incompatibleCrewMembers = new List<CrewMember>
            {
                new Pilot("pilot"),
                new FlightAttendant("flightAttendant")
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(incompatibleCrewMembers);

            Assert.False(result);
        }

        [Fact]
        public void Should_return_true_when_it_has_incompatible_crew_members_but_they_are_not_alone()
        {
            var crewMembers = new List<CrewMember>
            {
                new Pilot("pilot"),
                new FlightAttendant("flightAttendant"),
                new Officer("officer")
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.True(result);
        }

        [Fact]
        public void Should_return_false_when_it_has_more_than_two_crew_members_but_they_are_imcompatible()
        {
            var crewMembers = new List<CrewMember>
            {
                new Pilot("pilot"),
                new FlightAttendant("flightAttendant"),
                new FlightAttendant("flightAttendant")
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.False(result);
        }

        [Fact]
        public void Should_return_false_when_it_has_a_prisoner_and_does_not_have_a_policeman()
        {
            var crewMembers = new List<CrewMember>
            {
                new Prisoner("prisoner")
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.False(result);
        }

        [Fact]
        public void Should_return_true_when_it_has_a_prisoner_but_also_a_policeman()
        {
            var crewMembers = new List<CrewMember>
            {
                new Prisoner("prisoner"),
                new Policeman("policeman")
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.True(result);
        }

        [Fact]
        public void Should_return_true_when_all_crew_members_are_allowed()
        {
            var crewMembers = new List<CrewMember>
            {
                new Pilot("pilot"),
                new Officer("flightAttendant"),
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.True(result);
        }

        [Fact]
        public void Should_accept_null_crew_member_list()
        {
            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(null);
            Assert.True(result);
        }

        [Fact]
        public void Should_disregard_null_elements()
        {
            var crewMembers = new List<CrewMember>
            {
                new Pilot("pilot"),
                new Officer("flightAttendant"),
                null
            };

            var result = CrewChecker.CrewMembersAreAllowedToStayTogether(crewMembers);

            Assert.True(result);
        }
    }
}
