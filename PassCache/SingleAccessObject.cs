// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleAccessObject.cs" company="N/A">
//   Public domain
// </copyright>
// <summary>
//   Defines the SingleAccessObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PassCache
{
    /// <summary>
    /// Wraps an object to only allow it to be read once.
    /// After this the object is destroyed.
    /// </summary>
    /// <typeparam name="T">
    /// The object type to wrap
    /// </typeparam>
    public class SingleAccessObject<T> where T : class
    {
        /// <summary>
        /// The lock object, preventing the possibility of two threads getting
        /// access to the object.
        /// </summary>
        private readonly object accessLock = new object();

        /// <summary>
        /// The wrapped object.
        /// </summary>
        private readonly T obj;
        
        /// <summary>
        /// If the internal object was accessed
        /// </summary>
        private bool accessed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleAccessObject{T}"/> class.
        /// </summary>
        /// <param name="o">
        /// The object to wrap.
        /// </param>
        public SingleAccessObject(T o)
        {
            this.obj = o;
        }

        /// <summary>
        /// Attempts to get access to the object.
        /// </summary>
        /// <param name="objectInstance">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> object, if it has not been accessed or a null reference.
        /// </returns>
        public bool TryGet(out T objectInstance)
        {
            if (this.accessed)
            {
                return NullReference(out objectInstance);
            }

            lock (this.accessLock)
            {
                if (this.accessed)
                {
                    return NullReference(out objectInstance);
                }
                else
                {
                    this.accessed = true;
                    objectInstance = this.obj;
                    return true;
                }
            }
        }

        /// <summary>
        /// A shortcut for returning a null object reference.
        /// </summary>
        /// <param name="objectInstance">
        /// The object instance.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool NullReference(out T objectInstance)
        {
            objectInstance = null;
            return false;
        }
    }
}