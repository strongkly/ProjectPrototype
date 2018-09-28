using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Tools;

public class PeriodDestroy : MonoBehaviour
{
    uint timer;

    public DynamicGameObjectPool dyPool;

    public int remainSec;

    public void SetUp()
    {
        remainSec = 3;
        timer = TimerManager.Instance.NewTimer(3, () =>
        {
            remainSec = 0;
            dyPool.Reuse(this.gameObject);
        }, false, this.transform, 3);
    }

    public void Reset()
    {
        TimerManager.Instance.DisposeTimer(timer);
    }
}
