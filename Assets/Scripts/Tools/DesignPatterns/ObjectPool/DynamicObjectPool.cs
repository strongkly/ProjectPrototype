using System;
using UnityEngine;
using System.Collections.Generic;

namespace CrazyBox.Tools
{
    public abstract class IDynamicObjectPool<T> where T : new()
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

    public class DynamicObjectPool<T> : IDynamicObjectPool<T> where T : new()
    {
        public override int thresholdCount
        {
            get; protected set;
        }

        public override int UsableCount
        {
            get
            {
                return retired.Count;
            }
        }

        public int WorkingCount
        {
            get
            {
                return working.Count;
            }
        }

        HashSet<T> retired;
        HashSet<T> working;

        uint timer;

        bool startToReduce;

        float checkInterval;

        public Action<T> InitAction, ResetAction;

        public DynamicObjectPool()
        {

        }

        public DynamicObjectPool(int initSize, float checkInterval = 2f,
            Action<T> InitAction = null, Action<T> ResetAction = null)
        {
            this.thresholdCount = initSize;
            this.checkInterval = checkInterval;
            retired = new HashSet<T>();
            working = new HashSet<T>();
            this.InitAction = InitAction;
            this.ResetAction = ResetAction;
            InitPool();
        }

        protected virtual void InitPool()
        {
            for (int i = 0; i < thresholdCount; i++)
            {
                retired.Add(Create());
            }
        }

        public T Get()
        {
            PauseReduce();
            T result = default(T);
            if (retired.Count > 0)
            {
                result = retired.ElementAt(0);
            }
            else
            {
                result = Create();
            }
            Use(result);
            return result;
        }

        public override void Reuse(T disposedObject)
        {
            ResetAction.Invoke(disposedObject);
            Recycle(disposedObject);
            CheckIfNeedReduce();
        }

        protected virtual T Create()
        {
            T result = new T();
            InitAction(result);
            return result;
        }

        protected virtual void Dispose(T disposedObject)
        {
            retired.Remove(retired.ElementAt(0));
        }

        void Use(T ReusableObject)
        {
            if (retired.Contains(ReusableObject))
                retired.Remove(ReusableObject);
            working.Add(ReusableObject);
        }

        void Recycle(T ReusableObject)
        {
            if (working.Contains(ReusableObject))
                working.Remove(ReusableObject);
            retired.Add(ReusableObject);
        }

        void CheckIfNeedReduce()
        {
            if (!startToReduce && UsableCount > thresholdCount)
                StartToReduce();
        }

        void StartToReduce()
        {
            if (!startToReduce)
            {
                startToReduce = true;
                timer = TimerManager.Instance.NewTimer(checkInterval, Reduce, true, period: checkInterval);
            }
        }

        void Reduce()
        {
            if (UsableCount == thresholdCount)
            {
                PauseReduce();
                return;
            }
            Dispose(retired.ElementAt(0));
        }

        void PauseReduce()
        {
            if (startToReduce)
            {
                startToReduce = false;
                TimerManager.Instance.DisposeTimer(timer);
            }
        }
    }
}
