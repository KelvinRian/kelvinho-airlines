using kelvinho_airlines.Entities;
using kelvinho_airlines.Services.Interfaces;

namespace kelvinho_airlines.Services
{
    public class TripService : ITripService
    {
        public void DisembarkDriver(Place place)
        {
            throw new System.NotImplementedException();
        }

        public void DisembarkPassenger(Place place)
        {
            throw new System.NotImplementedException();
        }

        public void Move(Place origin, Place destiny)
        {
            if (origin.SmartFortwo != null && origin.SmartFortwo.Driver != null)
            {
                destiny.SetSmartFortwo(origin.SmartFortwo);
                origin.RemoveSmartFortwo();
            }
        }
    }
}
