using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CrazyBox.Components
{
    public class DragHandler : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
    {
        public DragHandlerEvent<PointerEventData> OnDragAction = new DragHandlerEvent<PointerEventData>();
        public DragHandlerEvent<PointerEventData> OnDropAction = new DragHandlerEvent<PointerEventData>();
        public DragHandlerEvent<PointerEventData> OnBeginAction = new DragHandlerEvent<PointerEventData>();
        public DragHandlerEvent<PointerEventData> OnEndAction = new DragHandlerEvent<PointerEventData>();

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
            if (OnDragAction != null)
                OnDragAction.Invoke(evtData);
        }

        public void OnDrop(PointerEventData evtData)
        {
            if (OnDropAction != null)
                OnDropAction.Invoke(evtData);
        }

        public void OnEndDrag(PointerEventData evtData)
        {
            if (OnEndAction != null)
                OnEndAction.Invoke(evtData);
        }
    }

    public class DragHandlerEvent<PED> : UnityEvent<PointerEventData>
    {
        public DragHandlerEvent() : base()
        {

        }
    }
}