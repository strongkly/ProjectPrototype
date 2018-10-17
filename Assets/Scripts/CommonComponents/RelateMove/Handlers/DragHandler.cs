using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    public UnityAction<PointerEventData> OnDragAction, OnDropAction, OnBeginAction;

    public static DragHandler Get(GameObject go)
    {
        DragHandler result = go.GetComponent<DragHandler>();
        if (result == null)
            result = go.AddComponent<DragHandler>();
        return result;
    }

    public void OnBeginDrag(PointerEventData evtData)
    {
        if (OnBeginAction != null)
            OnBeginAction.Invoke(evtData);
    }

    public void OnDrag(PointerEventData evtData)
    {
        if(OnDragAction != null)
            OnDragAction.Invoke(evtData);
    }

    public void OnDrop(PointerEventData evtData)
    {
        if (OnDropAction != null)
            OnDropAction(evtData);
    }
}
