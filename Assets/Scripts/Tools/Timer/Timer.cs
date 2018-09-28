using UnityEngine;
using UnityEngine.Events;

namespace CrazyBox.Tools
{
    public partial class TimerManager
    {
        public class Timer
        {
            public const uint NewTimerToken = 0;

            UnityAction periodAction, endAction, pauseAction, abortAction;
            bool isLoop, isPause;
            float totalSeconds, remainSeconds, period, remainPeriod;
            bool isRetire;
            Transform hostTransform;

            public uint Token
            {
                get;
                private set;
            }

            public float RemainSeconds
            {
                get
                {
                    return remainSeconds;
                }
            }

            public float Periods
            {
                get
                {
                    return period;
                }
            }

            public bool IsRetire
            {
                get
                {
                    return isRetire;
                }
            }

            public int RemainSecondsInInt
            {
                get
                {
                    return Mathf.RoundToInt(remainSeconds);
                }
            }

            public void SetTimer(float totalSeconds, float period, bool isLoop, Transform hostTransform,
                UnityAction periodAction = null, UnityAction endAction = null, UnityAction pauseAction = null,
                UnityAction abortAction = null)
            {
                this.totalSeconds = totalSeconds;
                this.period = period;
                this.isLoop = isLoop;
                this.hostTransform = hostTransform;
                this.periodAction = periodAction;
                this.endAction = endAction;
                this.pauseAction = pauseAction;
                this.abortAction = abortAction;
            }

            public Timer()
            {
                Token = NewTimerToken;
            }

            public void Start()
            {
                Reset();
                SetUp();
            }

            public void SetUp()
            {
                isPause = false;
                isRetire = false;
            }

            public void Pause()
            {
                if (!isPause)
                {
                    isPause = true;
                    if (pauseAction != null)
                        pauseAction();
                }
            }

            public void Recover()
            {
                if (isPause)
                    isPause = false;
            }

            public void Update()
            {
                CheckHostState();
                if (isRetire)
                    return;
                if (!isPause)
                {
                    remainPeriod -= Time.deltaTime;
                    remainSeconds -= Time.deltaTime;
                    if (remainPeriod <= 0)
                    {
                        remainPeriod = period;
                        if (periodAction != null)
                            periodAction.Invoke();
                    }
                    if (remainSeconds <= 0)
                    {
                        if (endAction != null)
                            endAction.Invoke();
                        if (isLoop == true)
                            Reset();
                        else
                            isRetire = true;
                    }
                }
            }

            public void CheckHostState()
            {
                if (hostTransform == null)
                    isRetire = true;
            }

            public void Reset()
            {
                remainSeconds = totalSeconds;
                remainPeriod = period;
            }

            public void Abort()
            {
                if (!isRetire)
                {
                    isRetire = true;
                    if (abortAction != null)
                        abortAction();
                }
            }

            public void SetToken(uint token)
            {
                this.Token = token;
            }
        }
    }
}