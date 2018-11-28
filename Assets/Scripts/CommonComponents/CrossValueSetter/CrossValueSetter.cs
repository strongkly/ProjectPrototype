using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CrazyBox.Components
{
    [ExecuteInEditMode]
    public class CrossValueSetter : MonoBehaviour
    {
        public RectTransform handler, bound;
        RelativeFix fix;
        Vector2 value;
        
        public Vector2 Value
        {
            get
            {
                return value;
            }
        }

        public UnityAction<Vector2> onValueChange;

        private void Start()
        {
            if (handler == null)
                handler = transform.GetChild(1) as RectTransform;
            if (bound == null)
                bound = transform.GetChild(0) as RectTransform;

            fix = RelativeFix.Get(handler);
            DragHandler.Get(handler.gameObject).OnDragAction = OnDrag;
            DragHandler.Get(handler.gameObject).OnDropAction = OnDrop;
            value = Vector2.zero;
        }

        void OnDrag(PointerEventData ped)
        {
            Vector3 pos = handler.localPosition;
            pos.Set(pos.x + ped.delta.x, pos.y + ped.delta.y, pos.z);
            handler.localPosition = pos;

            value.x = (bound.GetSelfWidth() / 2 + handler.localPosition.x) / bound.GetSelfWidth();
            value.y = (bound.GetSelfHeight() / 2 + handler.localPosition.y) / bound.GetSelfHeight();
            value.Set(Mathf.Clamp01(value.x), Mathf.Clamp01(value.y));
            if (onValueChange != null)
                onValueChange.Invoke(value);
        }

        void OnDrop(PointerEventData ped)
        {
            fix.FixPos(bound);
        }
    }
}