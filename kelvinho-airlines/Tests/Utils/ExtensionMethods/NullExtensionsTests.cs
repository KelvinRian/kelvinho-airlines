using kelvinho_airlines.Entities.Places;
using kelvinho_airlines.Utils.ExtensionMethods;
using Xunit;

namespace Tests.Utils.ExtensionMethods
{
    public class NullExtensionsTests
    {
        [Fact]
        public void should_return_true_when_object_is_null()
        {
            object obj = null;
            var result = obj.IsNull();
            Assert.True(result);
        }

        [Fact]
        public void should_return_false_when_object_is_not_null()
        {
            object obj = new Airplane();
            var result = obj.IsNull();
            Assert.False(result);
        }
    }
}
