#if WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    public static class EnumerableExtension
    {
        // Finds an item matching a predicate in the enumeration, much like List<T>.FindIndex()
        public static int FindIndex<T>(this IEnumerable<T> list, Predicate<T> finder)
        {
            int index = 0;
            foreach (var item in list)
            {
                if (finder(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
#endif