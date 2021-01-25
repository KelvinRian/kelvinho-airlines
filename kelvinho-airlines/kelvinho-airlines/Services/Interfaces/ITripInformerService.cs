using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;
using System.Collections.Generic;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ITripInformerService
    {
        void ShowBoardingInfo(params CrewMember[] crewMembers);
        void ShowDisembarkingInfo(IEnumerable<CrewMember> crewMembers);
        void ShowMovementInfo(Place origin, Place destiny);
        void ShowStartMessage();
        void ShowTripStateInfo(Place currentPlace, Place destinyPlace);
    }
}
