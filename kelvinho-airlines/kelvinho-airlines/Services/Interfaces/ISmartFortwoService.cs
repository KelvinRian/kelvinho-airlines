using kelvinho_airlines.Entities;
using System.Collections.Generic;

namespace kelvinho_airlines.Services.Interfaces
{
    public interface ISmartFortwoService
    {
        IEnumerable<CrewMember> Board(Place place, CrewMember driver, CrewMember passenger);
        IEnumerable<CrewMember> Disembark(Place place);
        CrewMember DisembarkDriver(Place place);
        CrewMember DisembarkPassenger(Place place);
        void Move(Place origin, Place destiny);
    }
}
