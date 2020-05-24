using kelvinho_airlines.Entities;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ISmartFortwoService
    {
        void Board(Place place, CrewMember driver, CrewMember passenger);
        void DisembarkDriver(Place place);
        void DisembarkPassenger(Place place);
        void Move(Place origin, Place destiny);
    }
}
