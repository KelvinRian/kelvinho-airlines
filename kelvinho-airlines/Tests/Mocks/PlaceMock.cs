using kelvinho_airlines.Entities;

namespace Tests.Mocks
{
    public class PlaceMock : Place
    {
        public PlaceMock() : base()
        {

        }

        public string GetNullSmartFortwoException()
            => _nullSmartFortwoException;
    }
}
