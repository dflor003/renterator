using System;

namespace Renterator.Common
{
    public static class Utils
    {
        private static readonly Random DefaultRandomGen = new Random();

        public static string NullArgumentCheck(string paramName, string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentNullException(paramName);
            }

            return item;
        }

        public static T NullArgumentCheck<T>(string paramName, T item)
            where T : class
        {
            if (item == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return item;
        }

        public static string NullCheck(string message, string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ApplicationException(message);
            }

            return item;
        }

        public static T NullCheck<T>(string message, T item)
            where T: class
        {
            if (item == null)
            {
                throw new ApplicationException(message);
            }

            return item;
        }

        public static string GenerateRandomId(
            string characters = @"abcdefghijklmnopqrstuvwxyz0123456789", 
            int length = 8, 
            Random randomGen = null)
        {
            Random rnd = randomGen ?? DefaultRandomGen;

            char[] resultChars = new char[length];
            for (int i = 0; i < resultChars.Length; i++)
            {
                resultChars[i] = characters[rnd.Next(characters.Length)];
            }

            return new string(resultChars);
        }
    }
}
