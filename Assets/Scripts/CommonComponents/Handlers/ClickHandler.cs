using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CrazyBox.Components
{
    public class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public ClickHandlerEvent<PointerEventData> OnClickAction 
            = new ClickHandlerEvent<PointerEventData>();

        public static ClickHandler Get(GameObject go)
        {
            ClickHandler result = go.GetComponent<ClickHandler>();
            if (result == null)
                result = go.AddComponent<ClickHandler>();
            return result;
        }

        public void OnPointerClick(PointerEventData ped)
        {
            if (OnClickAction != null)
                OnClickAction.Invoke(ped);
        }
    }

    public class ClickHandlerEvent<PED> : UnityEvent<PointerEventData>
    {
        public ClickHandlerEvent() : base()
        {

        }
    }
}
