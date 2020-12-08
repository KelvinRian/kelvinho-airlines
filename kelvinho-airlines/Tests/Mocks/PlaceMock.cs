using kelvinho_airlines.Entities;
using System.Collections.Generic;

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
