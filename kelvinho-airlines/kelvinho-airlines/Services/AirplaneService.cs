using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;
using System.Collections.Generic;

namespace kelvinho_airlines.Services
{
    public class AirplaneService : IPlaceService
    {
        public void Board(params CrewMember[] crewMembers)
        {
            throw new System.NotImplementedException();
        }

        public List<CrewMember> Disembark(params CrewMember[] crewMembers)
        {
            throw new System.NotImplementedException();
        }
    }
}
