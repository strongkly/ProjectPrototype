using System.Collections.Generic;

namespace CrazyBox.Tools
{ 
    public class ObjectPool<T> where T : new()
    {
        public int Count
        {
            get
            {
                return pool.Count;
            }
        }

        public int Size
        {
            get;
            private set;
        }

        HashSet<T> pool;

        public ObjectPool(int size)
        {
            pool = new HashSet<T>();
            this.Size = size;
            Initialize();
        }

        protected virtual void Initialize()
        {
            for (int i = 0; i < Size; i++)
            {
                pool.Add(Create());
            }
        }

        public T Get()
        {
            T result = default(T);
            if (pool.Count > 0)
            {
                result = pool.ElementAt(0);
                pool.Remove(result);
            }
            else
                result = Create();
            return result;
        }

        public void Recycle(T disposedObject)
        {
            pool.Add(disposedObject);
        }

        protected virtual T Create()
        {
            return new T();
        }
    }
}
