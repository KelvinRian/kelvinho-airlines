namespace kelvinho_airlines.Utils.ExtensionMethods
{
    public static class NullExtensions
    {
        public static bool IsNull(this object obj)
            => obj == null;

        public static bool IsNotNull(this object obj)
            => obj != null;
    }
}
