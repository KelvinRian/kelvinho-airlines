using kelvinho_airlines.Utils;
using Xunit;

namespace Tests.Utils
{
    public class TextHelperTests
    {
        [Fact]
        public void should_get_dividing_line()
        {
            Assert.Equal("*******************************************************************************************", TextHelper.DividingLine);
        }
    }
}
