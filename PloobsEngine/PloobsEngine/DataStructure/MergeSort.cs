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
using System;
using System.Collections.Generic;
 
namespace PloobsEngine.DataStructure
{
   /// <summary>
   /// Implementation of stable merge sort.
   /// </summary>
   class MergeSort
   {
       /// <summary>
       /// Sorts the specified entities.
       /// </summary>
       /// <typeparam name="T">is an ICOMPARABLE</typeparam>
       /// <param name="entities">The entities.</param>
      public static void Sort<T>(T[] entities) where T : IComparable<T>
      {
         if (entities.Length > 25)
         {
            int n = entities.Length / 2;
            T[] left = new T[n], right = new T[entities.Length - n];
            Array.Copy(entities, 0, left, 0, n);
            Array.Copy(entities, n, right, 0, entities.Length - n);
 
            Sort<T>(left);
            Sort<T>(right);
            Merge<T>(left, right, entities, DefaultComparer);
         }
         else
         {
            InsertionSort(entities, DefaultComparer);
         }
      }

      /// <summary>
      /// Sorts the specified entities.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="entities">The entities.</param>
      /// <param name="comparer">The comparer.</param>
      public static void Sort<T>(T[] entities, Comparison<T> comparer)
      {
         if (entities.Length > 25)
         {
            int n = entities.Length / 2;
            T[] left = new T[n], right = new T[entities.Length - n];
            Array.Copy(entities, 0, left, 0, n);
            Array.Copy(entities, n, right, 0, entities.Length - n);
 
            Sort<T>(left, comparer);
            Sort<T>(right, comparer);
            Merge<T>(left, right, entities, comparer);
         }
         else
         {
            InsertionSort(entities, comparer);
         }
      }

      /// <summary>
      /// Merges the specified left and right.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="left">The left.</param>
      /// <param name="right">The right.</param>
      /// <param name="entities">The entities.</param>
      /// <param name="comparer">The comparer.</param>
      private static void Merge<T>(T[] left, T[] right, T[] entities, Comparison<T> comparer)
      {
         int i = 0, j = 0, k = 0;
         int p = left.Length, q = right.Length;
         while (i < p && j < q)
         {
            int val = comparer(left[i], right[j]);
            if (val <= 0)
            {
               entities[k] = left[i];
               i++;
            }
            else
            {
               entities[k] = right[j];
               j++;
            }
            k++;
         }
         if (i == p)
            Array.Copy(right, j, entities, k, entities.Length - k);
         else
            Array.Copy(left, i, entities, k, entities.Length - k);
      }
 
      private static int DefaultComparer<T>(T left, T right) where T : IComparable<T>
      {
         return left.CompareTo(right);
      }
 
      private static void InsertionSort<T>(T[] array, Comparison<T> comparer)
      {
         for (int right = 1; right < array.Length; ++right)
         {
            for (int shift = right;
                 shift > 0 && comparer(array[shift], array[shift - 1]) < 0;
                 --shift)
            {
               T swap = array[shift];
               array[shift] = array[shift - 1];
               array[shift - 1] = swap;
            }
         }
      }
   }
}