#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PloobsEngine.Utils
{

#if !WINRT
    public static class EnumExtension
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return (from x in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
                    select (T)x.GetValue(null));
        }  


        public static T RandomEnum<T>()
        {
            List<T> values = new List<T>(
                from x in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
                select (T)x.GetValue(null));
            return values[new Random().Next(0, values.Count)];
        }  

    }
#endif

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