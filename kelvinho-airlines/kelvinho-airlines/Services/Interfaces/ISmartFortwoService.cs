using kelvinho_airlines.Entities;
using System.Collections.Generic;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ISmartFortwoService
    {
        List<CrewMember> Board(Place place, CrewMember driver, CrewMember passenger);
        void Disembark(Place place);
        void DisembarkDriver(Place place);
        void DisembarkPassenger(Place place);
        void Move(Place origin, Place destiny);
    }
}
