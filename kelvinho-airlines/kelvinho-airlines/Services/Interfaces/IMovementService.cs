using kelvinho_airlines.Dtos;
using kelvinho_airlines.Entities;
using kelvinho_airlines.Entities.CrewMembers;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface IMovementService
    {
        TripDto Move(Place currentPlace, Place destinyPlace);

        void PutInTheSmartFortwo(CrewMember driver, CrewMember passenger, Place currentPlace, Place destinyPlace);

        void DisembarkDriver(Place currentPlace, Place destinyPlace);

        void DisembarkPassenger(Place currentPlace, Place destinyPlace);

        void DisembarkAll(Place currentPlace, Place destinyPlace);
    }
}
