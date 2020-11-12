using kelvinho_airlines.Entities;
namespace kelvinho_airlines.Services.Interfaces
{
    public interface ISmartFortwoService
    {
        CrewMember DisembarkPassenger(Place place);
        void Move(Place origin, Place destiny);
    }
}
