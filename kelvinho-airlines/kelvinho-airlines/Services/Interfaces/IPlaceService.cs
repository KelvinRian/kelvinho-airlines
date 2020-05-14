using kelvinho_airlines.Entities;
using System.Collections.Generic;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface IPlaceService
    {
        void Board(params CrewMember[] crewMembers);
        List<CrewMember> Disembark(params CrewMember[] crewMembers);
    }   
}
