using System;
using CrazyBox.Tools;
using System.Collections.Generic;
using UnityEngine.Events;

namespace CrazyBox.Systems
{
    public class EventsManager : Singleton<EventsManager>
    {
        Dictionary<string, Delegate> allListeners;

        public EventsManager()
        {
            allListeners = new Dictionary<string, Delegate>();
        }

        #region AddListener
        public void AddListener(string evtName, UnityAction evtAction)
        {
            AddListener(evtName, (Delegate)evtAction);
        }

        public void AddListener<T>(string evtName, UnityAction<T> evtAction)
        {
            AddListener(evtName, (Delegate)evtAction);
        }

        public void AddListener<T1, T2>(string evtName, UnityAction<T1, T2> evtAction)
        {
            AddListener(evtName, (Delegate)evtAction);
        }

        public void AddListener<T1, T2, T3>(string evtName, UnityAction<T1, T2, T3> evtAction)
        {
            AddListener(evtName, (Delegate)evtAction);
        }

        public void AddListener<T1, T2, T3, T4>(string evtName, UnityAction<T1, T2, T3, T4> evtAction)
        {
            AddListener(evtName, (Delegate)evtAction);
        }

        public void AddListener(string evtName, Delegate evtAction)
        {
            Delegate Observer;
            if (allListeners.TryGetValue(evtName, out Observer))
            {
                Delegate.Combine(Observer, evtAction);
            }
            else
                allListeners.Add(evtName, evtAction);
        }
        #endregion

        #region Dispatch

        public void Dispatch(string evtName)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                var iter = observer.GetInvocationList().GetEnumerator();
                while (iter.MoveNext())
                {
                    (iter.Current as UnityAction).Invoke();
                }
            }
        }
        public void Dispatch<T>(string evtName, T arg)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                var iter = observer.GetInvocationList().GetEnumerator();
                while (iter.MoveNext())
                {
                    (iter.Current as UnityAction<T>).Invoke(arg);
                }
            }
        }

        public void Dispatch<T1, T2>(string evtName, T1 arg1, T2 arg2)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                var iter = observer.GetInvocationList().GetEnumerator();
                while (iter.MoveNext())
                {
                    (iter.Current as UnityAction<T1, T2>).Invoke(arg1, arg2);
                }
            }
        }

        public void Dispatch<T1, T2, T3>(string evtName, T1 arg1, T2 arg2, T3 arg3)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                var iter = observer.GetInvocationList().GetEnumerator();
                while (iter.MoveNext())
                {
                    (iter.Current as UnityAction<T1, T2, T3>).Invoke(arg1, arg2, arg3);
                }
            }
        }

        public void Dispatch<T1, T2, T3, T4>(string evtName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                var iter = observer.GetInvocationList().GetEnumerator();
                while (iter.MoveNext())
                {
                    (iter.Current as UnityAction<T1, T2, T3, T4>).Invoke(arg1, arg2, arg3, arg4);
                }
            }
        }
        #endregion

        #region RemoveListener
        public void RemoveListener<T>(string evtName, UnityAction<T> evtAction)
        {
            RemoveListener(evtName, (Delegate)evtAction);
        }

        public void RemoveListener<T1, T2>(string evtName, UnityAction<T1, T2> evtAction)
        {
            RemoveListener(evtName, (Delegate)evtAction);
        }

        public void RemoveListener<T1, T2, T3>(string evtName, UnityAction<T1, T2, T3> evtAction)
        {
            RemoveListener(evtName, (Delegate)evtAction);
        }

        public void RemoveListener<T1, T2, T3, T4>(string evtName, UnityAction<T1, T2, T3, T4> evtAction)
        {
            RemoveListener(evtName, (Delegate)evtAction);
        }

        public void RemoveListener(string evtName, Delegate evtAction)
        {
            Delegate observer;
            if (allListeners.TryGetValue(evtName, out observer))
            {
                Delegate operatedObserver = Delegate.Remove(observer, evtAction);
                if (operatedObserver == null)
                    allListeners.Remove(evtName);
                else
                    allListeners[evtName] = operatedObserver;
            }
        }
        #endregion
    }
}