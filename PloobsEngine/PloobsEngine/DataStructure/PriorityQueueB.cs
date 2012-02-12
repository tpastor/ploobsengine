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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace PloobsEngine.DataStructure
{
    #region Interfaces
    /// <summary>
    /// Priority queue specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPriorityQueue<T>
    {
        #region Methods
        /// <summary>
        /// Pushes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        int Push(T item);
        /// <summary>
        /// Pops an item.
        /// </summary>
        /// <returns>item popped</returns>
		T Pop();
        /// <summary>
        /// Peeks the top
        /// </summary>
        /// <returns></returns>
		T Peek();
        /// <summary>
        /// Updates the specified item i.
        /// </summary>
        /// <param name="i">The i.</param>
		void Update(int i);
        #endregion
    }
    #endregion

    /// <summary>
    /// Priority queue implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueueB<T> : IPriorityQueue<T>
    {
        #region Variables Declaration
        protected List<T>       InnerList = new List<T>();
		protected IComparer<T>  mComparer;
        #endregion

        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueueB&lt;T&gt;"/> class.
        /// </summary>
        public PriorityQueueB()
		{
			mComparer = Comparer<T>.Default;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueueB&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public PriorityQueueB(IComparer<T> comparer)
		{
			mComparer = comparer;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueueB&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="capacity">The capacity.</param>
		public PriorityQueueB(IComparer<T> comparer, int capacity)
		{
			mComparer = comparer;
			InnerList.Capacity = capacity;
		}
		#endregion

        #region Methods
        /// <summary>
        /// Switches the elements.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        protected void SwitchElements(int i, int j)
		{
			T h = InnerList[i];
			InnerList[i] = InnerList[j];
			InnerList[j] = h;
		}

        /// <summary>
        /// Called when [compare].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        protected virtual int OnCompare(int i, int j)
        {
            return mComparer.Compare(InnerList[i],InnerList[j]);
        }

        /// <summary>
        /// Push an object onto the PQ
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The index in the list where the object is _now_. This will change when objects are taken from or put onto the PQ.
        /// </returns>
		public int Push(T item)
		{
			int p = InnerList.Count,p2;
			InnerList.Add(item); // E[p] = O
			do
			{
				if(p==0)
					break;
				p2 = (p-1)/2;
				if(OnCompare(p,p2)<0)
				{
					SwitchElements(p,p2);
					p = p2;
				}
				else
					break;
			}while(true);
			return p;
		}

		/// <summary>
		/// Get the smallest object and remove it.
		/// </summary>
		/// <returns>The smallest object</returns>
		public T Pop()
		{
			T result = InnerList[0];
			int p = 0,p1,p2,pn;
			InnerList[0] = InnerList[InnerList.Count-1];
			InnerList.RemoveAt(InnerList.Count-1);
			do
			{
				pn = p;
				p1 = 2*p+1;
				p2 = 2*p+2;
				if(InnerList.Count>p1 && OnCompare(p,p1)>0) // links kleiner
					p = p1;
				if(InnerList.Count>p2 && OnCompare(p,p2)>0) // rechts noch kleiner
					p = p2;
				
				if(p==pn)
					break;
				SwitchElements(p,pn);
			}while(true);

            return result;
		}

		/// <summary>
		/// Notify the PQ that the object at position i has changed
		/// and the PQ needs to restore order.
		/// Since you dont have access to any indexes (except by using the
		/// explicit IList.this) you should not call this function without knowing exactly
		/// what you do.
		/// </summary>
		/// <param name="i">The index of the changed object.</param>
		public void Update(int i)
		{
			int p = i,pn;
			int p1,p2;
			do	
			{
				if(p==0)
					break;
				p2 = (p-1)/2;
				if(OnCompare(p,p2)<0)
				{
					SwitchElements(p,p2);
					p = p2;
				}
				else
					break;
			}while(true);
			if(p<i)
				return;
			do	   
			{
				pn = p;
				p1 = 2*p+1;
				p2 = 2*p+2;
				if(InnerList.Count>p1 && OnCompare(p,p1)>0) // links kleiner
					p = p1;
				if(InnerList.Count>p2 && OnCompare(p,p2)>0) // rechts noch kleiner
					p = p2;
				
				if(p==pn)
					break;
				SwitchElements(p,pn);
			}while(true);
		}

		/// <summary>
		/// Get the smallest object without removing it.
		/// </summary>
		/// <returns>The smallest object</returns>
		public T Peek()
		{
			if(InnerList.Count>0)
				return InnerList[0];
			return default(T);
		}

        /// <summary>
        /// Clears this instance.
        /// </summary>
		public void Clear()
		{
			InnerList.Clear();
		}

        /// <summary>
        /// Gets number of entities.
        /// </summary>
		public int Count
		{
			get{ return InnerList.Count; }
		}

        /// <summary>
        /// Removes the item 
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveLocation(T item)
        {
            int index = -1;
            for(int i=0; i<InnerList.Count; i++)
            {
                
                if (mComparer.Compare(InnerList[i], item) == 0)
                    index = i;
            }

            if (index != -1)
                InnerList.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> at the specified index.
        /// </summary>
        public T this[int index]
        {
            get { return InnerList[index]; }
            set 
            { 
                InnerList[index] = value;
				Update(index);
            }
        }
		#endregion
    }
}