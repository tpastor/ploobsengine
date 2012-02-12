using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Some extensions to IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Copies to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="array">The array.</param>
        /// <param name="startIndex">The start index.</param>
        public static void CopyTo<T>(this IEnumerable<T> source, T[] array, int startIndex)
        {
            int lowerBound = array.GetLowerBound(0);
            int upperBound = array.GetUpperBound(0);
            if (startIndex < lowerBound)
                throw new ArgumentOutOfRangeException("startIndex", "The start index must be greater than or equal to the array lower bound");
            if (startIndex > upperBound)
                throw new ArgumentOutOfRangeException("startIndex", "The start index must be less than or equal to the array upper bound");

            int i = 0;
            foreach (var item in source)
            {
                if (startIndex + i > upperBound)
                    throw new ArgumentException("The array capacity is insufficient to copy all items from the source sequence");
                array[startIndex + i] = item;
                i++;
            }
        }

        /// <summary>
        /// Index of
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T item)
        {
            var entry = source.Select((x, i) => new { Value = x, Index = i })
                        .Where(x => object.Equals(x.Value, item))
                        .FirstOrDefault();
            return entry != null ? entry.Index : -1;
        }
    }
}
