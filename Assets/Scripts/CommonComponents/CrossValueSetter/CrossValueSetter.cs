using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CrazyBox.Components
{
    [ExecuteInEditMode]
    public class CrossValueSetter : MonoBehaviour
    {
        public RectTransform handler, bound;
        [Tooltip("asign this will enable click event detecting")]
        public Camera UICamera;
        RelativeFix fix;
        Vector2 value;
        
        public Vector2 Value
        {
            get
            {
                return value;
            }
        }

        public CorssValueSetterEvent onValueChange;

        public void Invoke(Vector2 value)
        {
            this.value = value;
            if (onValueChange != null)
                onValueChange.Invoke(value);
        }

        private void Start()
        {
            if (handler == null)
                handler = transform.GetChild(1) as RectTransform;
            if (bound == null)
                bound = transform.GetChild(0) as RectTransform;

            onValueChange = new CorssValueSetterEvent();
            fix = RelativeFix.Get(handler);
            DragHandler.Get(handler.gameObject).OnDragAction.AddListener(OnDrag);
            DragHandler.Get(handler.gameObject).OnDropAction.AddListener(OnDrop);
            ClickHandler.Get(bound.gameObject).OnClickAction.AddListener(OnClick);
            value = Vector2.zero;
        }

        void OnDrag(PointerEventData ped)
        {
            Vector3 pos = handler.localPosition;
            pos.Set(pos.x + ped.delta.x, pos.y + ped.delta.y, pos.z);
            handler.localPosition = pos;

            Refresh();
        }

        void Refresh()
        {
            CaculateValue();
            Invoke(this.value);
        }

        void CaculateValue()
        {
            value.x = (bound.GetSelfWidth() / 2 + handler.localPosition.x) / bound.GetSelfWidth();
            value.y = (bound.GetSelfHeight() / 2 + handler.localPosition.y) / bound.GetSelfHeight();
            value.Set(Mathf.Clamp01(value.x), Mathf.Clamp01(value.y));
        }

        void OnDrop(PointerEventData ped)
        {
            fix.LimitFixPos(bound);
        }

        void OnClick(PointerEventData ped)
        {
            if (UICamera != null)
            {
                Vector3 pos = UICamera.ScreenToWorldPointInRectTransform(ped.pressPosition,
                    bound);
                pos = handler.parent.InverseTransformPoint(pos);
                pos.z = 0;
                handler.localPosition = pos;

                Refresh();
                fix.LimitFixPos(bound);
            }
        }
    }

    public class CorssValueSetterEvent : UnityEvent<Vector2>
    {

    }
}