using kelvinho_airlines.Entities;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ITripService
    {
        void DisembarkDriver(Place place);
        void DisembarkPassenger(Place place);
        void Move(Place origin, Place destiny);
    }
}
