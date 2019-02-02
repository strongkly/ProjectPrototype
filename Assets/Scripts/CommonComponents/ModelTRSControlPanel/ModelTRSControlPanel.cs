using UnityEngine;
using UnityEngine.EventSystems;

namespace CrazyBox.Components.Functional
{
    [RequireComponent(typeof(RectTransform))]
    public class ModelTRSControlPanel : MonoBehaviour
    {
        [SerializeField]
        Transform Target;

        [SerializeField]
        string pivotTransName;

        [SerializeField]
        bool IsPivotRotate;

        [SerializeField]
        [Range(0, 1)]
        float strength;

        public Vector3 Pivot { get; private set; }

        private void Start()
        {
            DragHandler.Get(gameObject).OnBeginAction.AddListener(OnBeginDrag);
            DragHandler.Get(gameObject).OnDragAction.AddListener(OnDrag);
        }

        #region callbacks
        void OnDrag(PointerEventData ped)
        {
            if (!IsPivotRotate)
                RotateAroundY(ped.delta.x);
            else
                RotateAroundPivotAndAxisY(ped.delta.x);
        }
        void OnBeginDrag(PointerEventData ped)
        {
            if (IsPivotRotate)
            {
                Transform pivotTrans = null;
                if (Target.TryFindTransformFromDescendantsByName(ref pivotTrans, pivotTransName))
                {
                    Pivot = pivotTrans.position;
                }
            }
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

        public void SetPivotRotate(string pivotTrans, bool pivotRotate = true)
        {
            this.IsPivotRotate = pivotRotate;
            pivotTransName = pivotTrans;
        }

        public void RotateAroundY(float pixelDis, float strength = 1)
        {
            if (strength != 1)
                this.strength = Mathf.Clamp01(strength);
            if (Target != null)
                Target.Rotate(Vector3.up, - pixelDis * this.strength);
        }

        public void RotateAroundPivotAndAxisY(float pixelDis, float strength = 1)
        {
            if (strength != 1)
                this.strength = Mathf.Clamp01(strength);
            if (Pivot == null)
                Pivot = transform.position;
            if (Target != null)
                Target.RotateAround(Pivot, Vector3.up, -pixelDis * this.strength);
        }
    }
}
