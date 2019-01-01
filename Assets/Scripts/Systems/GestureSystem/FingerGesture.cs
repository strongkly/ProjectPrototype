using UnityEngine;
using UnityEngine.Events;

public class FingerGesture : MonoBehaviour
{
    private Vector2 prevDist = new Vector2(0, 0);
    private Vector2 curDist = new Vector2(0, 0);

    FingerGestrueEvent<float> OnZoomIn, OnZoomOut;
    FingerGestrueEvent OnGestureEnd;

    bool isOn = false;
    public bool IsOn
    {
        set
        {
            isOn = value;
            if (!isOn)
            {
                ClearAllEventListeners();
            }
        }
        get
        {
            return isOn;
        }
    }

    private void Awake()
    {
        OnZoomIn = new FingerGestrueEvent<float>();
        OnZoomOut = new FingerGestrueEvent<float>();
        OnGestureEnd = new FingerGestrueEvent();
    }

    void Update()
    {
        if (isOn)
            CheckForMultiTouch();
    }

    void CheckForMultiTouch()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved &&
            Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; 
            prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) -
                (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); 
            float touchDelta = curDist.magnitude - prevDist.magnitude;
            if (touchDelta > 0)
            {
                if (OnZoomOut != null) OnZoomOut.Invoke(touchDelta);
            }
            else if (touchDelta < 0)
            {
                if (OnZoomIn != null) OnZoomIn.Invoke(touchDelta);
            }
        }
        else if (Input.touchCount == 2 &&
            (Input.GetTouch(0).phase == TouchPhase.Ended ||
            Input.GetTouch(0).phase == TouchPhase.Canceled ||
            Input.GetTouch(1).phase == TouchPhase.Ended ||
            Input.GetTouch(1).phase == TouchPhase.Canceled))
        {
            if (OnGestureEnd != null) OnGestureEnd.Invoke();
        }
    }

    public void ClearAllEventListeners()
    {
        OnZoomIn.RemoveAllListeners();
        OnZoomOut.RemoveAllListeners();
        OnGestureEnd.RemoveAllListeners();
    }

    #region API
    public void SetZoomInCallBack(UnityAction<float> OnZoomIn)
    {
        if (OnZoomIn != null) this.OnZoomIn.AddListener(OnZoomIn);
    }

    public void RemoveZoomInCallBack(UnityAction<float> OnZoomIn)
    {
        if(OnZoomIn != null) this.OnZoomIn.RemoveListener(OnZoomIn);
    }

    public void SetZoomOutCallBack(UnityAction<float> OnZoomOut)
    {
        if (OnZoomOut != null) this.OnZoomOut.AddListener(OnZoomOut);
    }

    public void RemoveZoomOutCallBack(UnityAction<float> OnZoomOut)
    {
        if (OnZoomOut != null) this.OnZoomOut.RemoveListener(OnZoomOut);
    }

    public void SetGestureEndCallBack(UnityAction OnGestureEnd)
    {
        if (OnGestureEnd != null) this.OnGestureEnd.AddListener(OnGestureEnd);
    }

    public void RemoveGestureEndCallBack(UnityAction OnGestureEnd)
    {
        if (OnGestureEnd != null) this.OnGestureEnd.RemoveListener(OnGestureEnd);
    }
    #endregion
}

public class FingerGestrueEvent : UnityEvent
{
    
}

public class FingerGestrueEvent<T> : UnityEvent<T>
{
    
}