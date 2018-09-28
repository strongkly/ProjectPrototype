using System.Collections.Generic;

namespace CrazyBox.Tools
{
    public abstract class IReusable
    {
        public bool IsUse
        {
            get;
            private set;
        }

        public void SetUse(bool isUse)
        {
            IsUse = isUse;
        }
    }

    public abstract class IDynamicPool<T> where T:IReusable, new()
    {
        public virtual int thresholdCount
        {
            get; protected set;
        }

        public virtual int UsableCount
        {
            get;
            protected set;
        }

        public abstract void Reuse(T disposedObject);
    }

    public class DynamicPool<T> : IDynamicPool<T> where T : IReusable, new()
    {
        public override int thresholdCount
        {
            get; protected set;
        }

        public override int UsableCount
        {
            get; protected set;
        }

        HashSet<T> pool;

        uint timer;

        bool startToReduce;

        float checkInterval;

        public DynamicPool(int initSize, float checkInterval = 2f)
        {
            this.thresholdCount = initSize;
            this.checkInterval = checkInterval;
            pool = new HashSet<T>();
            for (int i = 0; i < initSize; i++)
            {
                pool.Add(Create());
            }
            UsableCount = initSize;
        }

        public T Get()
        {
            var iter = pool.GetEnumerator();
            T result = default(T);
            while (iter.MoveNext())
            {
                if (!iter.Current.IsUse)
                {
                    result = iter.Current;
                    UsableCount--;
                    break;
                }
            }
            if (result == default(T))
                pool.Add(Create());

            result.SetUse(true);
            return result;
        }

        public override void Reuse(T disposedObject)
        {
            disposedObject.SetUse(false);
            UsableCount++;
            CheckIfNeedReduce();
        }

        public int GetUsableCount()
        {
            int result = 0;
            var iter = pool.GetEnumerator();
            while (iter.MoveNext())
            {
                if (!iter.Current.IsUse)
                    result++;
            }
            return result;
        }

        protected virtual T Create()
        {
            return new T();
        }

        void CheckIfNeedReduce()
        {
            if (!startToReduce && UsableCount > thresholdCount)
                StartToReduce();
        }

        void StartToReduce()
        {
            startToReduce = true;
            timer = TimerManager.Instance.NewTimer(checkInterval, Reduce, true, period: checkInterval);
        }

        void Reduce()
        {
            if (pool.Count == thresholdCount)
            {
                PauseReduce();
                return;
            }
            var iter = pool.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.IsUse == false)
                {
                    pool.Remove(iter.Current);
                    UsableCount--;
                    return;
                }
            }
        }

        void PauseReduce()
        {
            startToReduce = false;
            TimerManager.Instance.DisposeTimer(timer);
        }
    }
}