namespace PassCache
{
    public class SingleAccessObject<T> where T : class
    {
        private readonly object _accessLock = new object();

        private readonly T _obj;

        private bool _accessed;

        public SingleAccessObject(T o)
        {
            _obj = o;
        }

        public bool TryGet(out T objectInstance)
        {
            if (_accessed)
            {
                return NullReference(out objectInstance);
            }

            lock (_accessLock)
            {
                if (_accessed)
                {
                    return NullReference(out objectInstance);
                }
                else
                {
                    _accessed = true;
                    objectInstance = _obj;
                    return true;
                }
            }
        }

        private static bool NullReference(out T objectInstance)
        {
            objectInstance = null;
            return false;
        }
    }
}
