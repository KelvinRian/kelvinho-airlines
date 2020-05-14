using kelvinho_airlines.Entities;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ISmartFortwoService
    {
        void Board(CrewMember driver, CrewMember passenger);
        CrewMember DisembarkDriver(CrewMember driver);
        CrewMember DisembarkPassenger(CrewMember passenger);
        void Move(Place location);

    }
}
