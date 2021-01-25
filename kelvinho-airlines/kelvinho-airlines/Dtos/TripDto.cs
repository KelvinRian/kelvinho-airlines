using kelvinho_airlines.Entities;

namespace kelvinho_airlines.Dtos
{
    public class TripDto
    {
        public Place CurrentPlace { get; set; }
        public Place DestinyPlace { get; set; }

        public TripDto(Place currentPlace, Place destinyPlace)
        {
            CurrentPlace = currentPlace;
            DestinyPlace = destinyPlace;
        }
    }
}
