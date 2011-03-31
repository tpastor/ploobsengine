using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PloobsEngine.DataStructure
{
    /// <summary>
    /// A collection that maintains a set of class instances to allow for recycling
    /// instances and minimizing the effects of garbage collection.
    /// </summary>
    /// <typeparam name="T">The type of object to store in the Pool. Pools can only hold class types.</typeparam>
    public class Pool<T> where T : class
    {
        // the amount to enlarge the items array if New is called and there are no free items
        private const int resizeAmount = 20;

        // whether or not the pool is allowed to resize
        private bool canResize;

        // the actual items of the pool
        private T[] items;

        // used for checking if a given object is still valid
        private readonly Predicate<T> validate;

        // used for allocating instances of the object
        private readonly Func<T> allocate;

        /// <summary>
        /// Gets or sets a delegate used for initializing objects before returning them from the New method.
        /// </summary>
        public Action<T> Initialize { get; set; }

        /// <summary>
        /// Gets or sets a delegate that is run when an object is moved from being valid to invalid
        /// in the CleanUp method.
        /// </summary>
        public Action<T> Deinitialize { get; set; }

        /// <summary>
        /// Gets the number of valid objects in the pool.
        /// </summary>
        public int ValidCount { get { return items.Length - InvalidCount; } }

        /// <summary>
        /// Gets the number of invalid objects in the pool.
        /// </summary>
        public int InvalidCount { get; private set; }

        /// <summary>
        /// Returns a valid object at the given index. The index must fall in the range of [0, ValidCount].
        /// </summary>
        /// <param name="index">The index of the valid object to get</param>
        /// <returns>A valid object found at the index</returns>
        public T this[int index]
        {
            get
            {
                index += InvalidCount;

                if (index < InvalidCount || index >= items.Length)
                    throw new IndexOutOfRangeException("The index must be less than or equal to ValidCount");

                return items[index];
            }
        }

        /// <summary>
        /// Creates a new pool with a specific starting size.
        /// </summary>
        /// <param name="initialSize">The initial size of the pool.</param>
        /// <param name="resizes">Whether or not the pool is allowed to increase its size as needed.</param>
        /// <param name="validateFunc">A predicate used to determine if a given object is still valid.</param>
        /// <param name="allocateFunc">A function used to allocate an instance for the pool.</param>
        public Pool(int initialSize, bool resizes, Predicate<T> validateFunc, Func<T> allocateFunc)
        {
            // validate some parameters
            if (initialSize < 1)
                throw new ArgumentOutOfRangeException("initialSize", "initialSize must be at least 1.");
            if (validateFunc == null)
                throw new ArgumentNullException("validateFunc");
            if (allocateFunc == null)
                throw new ArgumentNullException("allocateFunc");

            canResize = resizes;

            // create our items array
            items = new T[initialSize];
            InvalidCount = items.Length;

            // store our delegates
            validate = validateFunc;
            allocate = allocateFunc;
        }

        /// <summary>
        /// Cleans up the pool by checking each valid object to ensure it is still actually valid.
        /// </summary>
        public void CleanUp()
        {
            for (int i = InvalidCount; i < items.Length; i++)
            {
                T obj = items[i];

                // if it's still valid, keep going
                if (validate(obj))
                    continue;

                // otherwise if we're not at the start of the invalid objects, we have to move
                // the object to the invalid object section of the array
                if (i != InvalidCount)
                {
                    items[i] = items[InvalidCount];
                    items[InvalidCount] = obj;
                }

                // clean the object if desired
                if (Deinitialize != null)
                    Deinitialize(obj);

                InvalidCount++;
            }
        }

        /// <summary>
        /// Returns a new object from the Pool.
        /// </summary>
        /// <returns>The next object in the pool if available, null if all instances are valid.</returns>
        public T New()
        {
            // if we're out of invalid instances...
            if (InvalidCount == 0)
            {
                // if we can't resize, then we can't give the user back any instance
                if (!canResize)
                    return null;

                Debug.WriteLine("Resizing pool. Old size: " + items.Length + ". New size: " + (items.Length + resizeAmount));

                // create a new array with some more slots and copy over the existing items
                T[] newItems = new T[items.Length + resizeAmount];
                for (int i = items.Length - 1; i >= 0; i--)
                    newItems[i + resizeAmount] = items[i];
                items = newItems;

                // move the invalid count based on our resize amount
                InvalidCount += resizeAmount;
            }

            // decrement the counter
            InvalidCount--;

            // get the next item in the list
            T obj = items[InvalidCount];

            // if the item is null, we need to allocate a new instance
            if (obj == null)
            {
                obj = allocate();

                if (obj == null)
                    throw new InvalidOperationException("The pool's allocate method returned a null object reference.");

                items[InvalidCount] = obj;
            }

            // initialize the object if a delegate was provided
            if (Initialize != null)
                Initialize(obj);

            return obj;
        }
    }
}
