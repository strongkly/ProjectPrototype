using UnityEngine;
using UnityEngine.EventSystems;

namespace CrazyBox.Components.Functional
{
    [RequireComponent(typeof(RectTransform))]
    public class ModelTRSControlPanel : MonoBehaviour
    {
        public Transform Target { get; private set; }

        [SerializeField]
        [Range(0, 1)]
        float strength;

        private void Start()
        {
            DragHandler.Get(gameObject).OnDragAction.AddListener(OnDrag);
        }

        #region callbacks
        void OnDrag(PointerEventData ped)
        {
            RotateAroundY(ped.delta.x);
        }
        #endregion

        public Transform SetTargetModel(Transform target)
        {
            Target = target;
            return Target;
        }

        public void SetStrength(float strength = 1)
        {
            this.strength = strength;
        }

        public void RotateAroundY(float pixelDis, float strength = 1)
        {
            if (strength != 1)
                this.strength = Mathf.Clamp01(strength);
            if (Target != null)
                Target.Rotate(Vector3.up, - pixelDis * this.strength);
        }
    }
}
