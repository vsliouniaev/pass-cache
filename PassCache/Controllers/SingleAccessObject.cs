namespace PassCache.Controllers
{
    public class SingleAccessObject<T> where T : class
    {
        private bool _retrieved = false;
        private readonly object _lock = new object();
        private readonly T _obj;

        public SingleAccessObject(T o)
        {
            _obj = o;
        }

        public bool TryGet(out T obj)
        {
            lock (_lock)
            {
                if (!_retrieved)
                {
                    _retrieved = true;
                    obj = _obj;
                    return true;
                }
                else
                {
                    obj = null;
                    return false;
                }
            }
        }
    }
}