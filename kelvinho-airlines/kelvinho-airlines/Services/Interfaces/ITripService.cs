using kelvinho_airlines.Entities;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ITripService
    {
        void BoardTheSmartFortwo(Place place, CrewMember driver, CrewMember passenger);
        void DisembarkDriverFromSmartFortwo(Place place);
        void DisembarkPassengerFromSmartFortwo(Place place);
        void Move(Place origin, Place destiny);
    }
}
