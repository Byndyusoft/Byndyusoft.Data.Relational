using System;

namespace Byndyusoft.Data.Relational
{
    internal static class Guard
    {
        public static T NotNull<T>(T value, string paramName)
        {
            return value is null ? throw new ArgumentNullException(paramName) : value;
        }

        public static string NotNullOrWhiteSpace(string value, string paramName)
        {
            return string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(paramName) : value;
        }
    }
}