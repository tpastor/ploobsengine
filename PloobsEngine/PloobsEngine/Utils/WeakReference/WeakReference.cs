#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if WINDOWS
using System.Runtime.Serialization;
#endif
namespace PloobsEngine.Utils
{


        
    /// <summary>
    /// Weak reference Strong type
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if WINDOWS
    [Serializable]
#endif
    public class WeakReference<T> : System.WeakReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public WeakReference(T target)
            : base(target)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="trackResurrection">if set to <c>true</c> [track resurrection].</param>
        public WeakReference(T target, bool trackResurrection)
            : base(target, trackResurrection)
        {
        }
#if WINDOWS
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference"/> object.</param>
        /// <param name="context">(Reserved) Describes the source and destination of the serialized stream specified by <paramref name="info"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="info"/> is null. </exception>
        public WeakReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        /// <summary>
        /// Gets or sets the object (the target) referenced by the current <see cref="T:System.WeakReference"/> object.
        /// </summary>
        /// <returns>null if the object referenced by the current <see cref="T:System.WeakReference"/> object has been garbage collected; otherwise, a reference to the object referenced by the current <see cref="T:System.WeakReference"/> object.</returns>
        ///   
        /// <exception cref="T:System.InvalidOperationException">The reference to the target object is invalid. This exception can be thrown while setting this property if the value is a null reference or if the object has been finalized during the set operation.</exception>
        public new T Target
        {
            get
            {
                return (T)base.Target;
            }
        }
    }
}
#else
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PloobsEngine.Utils
{
        
    /// <summary>
    /// Weak reference Strong type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeakReference<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public WeakReference(T target)            
        {
            weakReference = new System.WeakReference(target);
        }


        System.WeakReference weakReference = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="trackResurrection">if set to <c>true</c> [track resurrection].</param>
        public WeakReference(T target, bool trackResurrection)            
        {
            weakReference = new System.WeakReference(target, trackResurrection);
        }

        /// <summary>
        /// Gets or sets the object (the target) referenced by the current <see cref="T:System.WeakReference"/> object.
        /// </summary>
        /// <returns>null if the object referenced by the current <see cref="T:System.WeakReference"/> object has been garbage collected; otherwise, a reference to the object referenced by the current <see cref="T:System.WeakReference"/> object.</returns>
        ///   
        /// <exception cref="T:System.InvalidOperationException">The reference to the target object is invalid. This exception can be thrown while setting this property if the value is a null reference or if the object has been finalized during the set operation.</exception>
        public new T Target
        {
            get
            {
                return (T)weakReference.Target;
            }
        }


        // Summary:
        //     Gets an indication whether the object referenced by the current System.WeakReference
        //     object has been garbage collected.
        //
        // Returns:
        //     true if the object referenced by the current System.WeakReference object
        //     has not been garbage collected and is still accessible; otherwise, false.
        public virtual bool IsAlive { get { return weakReference.IsAlive; } }
        //
        //
        // Summary:
        //     Gets an indication whether the object referenced by the current System.WeakReference
        //     object is tracked after it is finalized.
        //
        // Returns:
        //     true if the object the current System.WeakReference object refers to is tracked
        //     after finalization; or false if the object is only tracked until finalization.
        public virtual bool TrackResurrection { get {return weakReference.TrackResurrection;} }
    }
}

#endif