using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CrazyBox.Tools
{
    public partial class TimerManager : ScriptSingleton<TimerManager>
    {
        Dictionary<uint, Timer> allTimers = new Dictionary<uint, Timer>();
        uint token = 0;
        ObjectPool<Timer> pool = new ObjectPool<Timer>(10);

        public int Count{
            get
            {
                return allTimers.Count;
            }
        }

        public uint NewTimer(float totalSeconds, UnityAction periodAction, bool isLoop = false,
            Transform hostTransform = null, float period = 1, UnityAction endAction = null,
            UnityAction pauseAction = null, UnityAction abortAction = null)
        {
            if (hostTransform == null)
                hostTransform = GameObject.Find("Main Camera").transform;//TODO

            Timer timer;
            uint result = TryReuseTimer(out timer);
            timer.SetTimer(totalSeconds, period, isLoop, hostTransform, periodAction, endAction,
                pauseAction, abortAction);
            timer.Start();

            return result;
        }

        public void DisposeTimer(uint token)
        {
            if (allTimers.ContainsKey(token))
            {
                allTimers[token].Abort();
                pool.Recycle(allTimers[token]);
            }
            else
                throw new System.Exception("待弃用Timer 没有被TimerManager管理");
        }

        public void Update()
        {
            var iter = allTimers.Keys.GetEnumerator();
            while (iter.MoveNext())
            {
                if (!allTimers[iter.Current].IsRetire)
                {
                    allTimers[iter.Current].Update();
                    if (allTimers[iter.Current].IsRetire)
                        DisposeTimer(iter.Current);
                }
            }
        }

        uint TryReuseTimer(out Timer timer)
        {
            uint result;
            timer = pool.Get();
            if (IsNewTimer(timer))
            {
                result = ++token;
                allTimers.Add(token, timer);
                timer.SetToken(token);
            }
            else
            {
                result = timer.Token;
            }

            return result;
        }

        bool IsNewTimer(Timer timer)
        {
            return timer.Token == Timer.NewTimerToken;
        }
    }
}