using System;

namespace SalesTaxManager.Utils
{
    public static class Guard
    {
        public static T AgainstLessThan<T>(T upperLimit, T value, string message) where T : IComparable<T>
        {
            if (value.CompareTo(upperLimit) < 0)
                throw new ArgumentOutOfRangeException(
                    $"ERROR : {message} (parameter error {value})");
            return value;
        }
        public static void AgainstEmpty(string value, string message)
        {
            if (value == "")
                throw new ArgumentException(message);
        }

        public static T AgainstNull<T>(T value, string message)
          where T : class
        {
            if (value == null)
                throw new ArgumentNullException(message);
            return value;
        }
    }
}
