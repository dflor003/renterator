using System;
using System.Collections.Generic;

namespace Renterator.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Performs an action on each element in the enumerable and returns the same enumerable.
        /// </summary>
        /// <typeparam name="T">The type of element in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to perform actions on.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns>The original enumerable.</returns>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Forces the evaluation of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to force.</param>
        public static void Force<T>(this IEnumerable<T> enumerable)
        {
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                }
            }
        }
    }
}
