using System;

namespace FlatDir.Method
{
    internal static class RandomString
    {
        private static Random rng = new Random();
        public static string Create(int stringLength)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyz";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rng.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
