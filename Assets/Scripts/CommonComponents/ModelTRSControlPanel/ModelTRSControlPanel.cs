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
        Camera zoomCamera;

        [SerializeField]
        Transform pivotTrans;

        [SerializeField]
        bool IsPivotRotate;

        [SerializeField]
        [Range(0, 1)]
        float rotateStrength;

        [SerializeField]
        [Range(0, 1)]
        float zoomStrength;

        [SerializeField]
        bool enableZoom = true;

        [SerializeField]
        bool enableRotate = true;

        [SerializeField]
        Vector3 zoomBoundaryInPos, zoomBoundaryOutPos;

        Quaternion zoomInRot = Quaternion.identity, zoomOutRot = Quaternion.identity;

        public Vector3 Pivot { get; private set; }

        public Camera ZoomCamera { get { return zoomCamera; } }

        private void Start()
        {
            DragHandler.Get(gameObject).OnBeginAction.AddListener(OnBeginDrag);
            DragHandler.Get(gameObject).OnDragAction.AddListener(OnDrag);
        }

        #region callbacks
        void OnDrag(PointerEventData ped)
        {
            if (enableRotate)
            {
                if (!IsPivotRotate)
                    RotateAroundY(ped.delta.x);
                else
                    RotateAroundPivotAndAxisY(ped.delta.x);
            }
            if (enableZoom)
            {
                ZoomWithBoundary(this.zoomBoundaryInPos, this.zoomBoundaryOutPos, ped.delta.y);
            }
        }

        void OnBeginDrag(PointerEventData ped)
        {
            if (IsPivotRotate)
            {
                if (pivotTrans != null)
                    Pivot = pivotTrans.position;
            }
        }
        #endregion

        public Transform SetTargetModel(Transform target)
        {
            Target = target;
            return Target;
        }

        public void SetRoteteStrength(float strength = 1)
        {
            this.rotateStrength = Mathf.Clamp01(strength);
        }

        public void SetZoomStrength(float strength = 1)
        {
            this.zoomStrength = Mathf.Clamp01(strength);
        }

        public void SetPivotRotate(Transform pivotTrans, bool pivotRotate = true)
        {
            this.IsPivotRotate = pivotRotate;
            this.pivotTrans = pivotTrans;
        }

        public void SetZoomBoundary(Vector3 boundaryInPos, Vector3 boundaryOutPos)
        {
            zoomBoundaryOutPos = boundaryOutPos;
            zoomBoundaryInPos = boundaryInPos;
        }

        public void SetZoomBoundaryQuaterion(Quaternion zoomInRot, Quaternion zoomOutRot)
        {
            this.zoomInRot = zoomInRot;
            this.zoomOutRot = zoomOutRot;
        }

        public void SetZoomCamera(Camera zoomCamera)
        {
            this.zoomCamera = zoomCamera;
        }

        public void RotateAroundY(float pixelDis, float strength = 1)
        {
            if (strength != 1)
                SetRoteteStrength(strength);
            if (Target != null)
                Target.Rotate(Vector3.up, - pixelDis * this.rotateStrength);
        }

        public void RotateAroundPivotAndAxisY(float pixelDis, float strength = 1)
        {
            if (strength != 1)
                SetRoteteStrength(strength);
            if (Pivot == null)
                Pivot = transform.position;
            if (Target != null)
                Target.RotateAround(Pivot, Vector3.up, -pixelDis * this.rotateStrength);
        }

        public void ZoomWithBoundary(Vector3 inPos, Vector3 outPos,
            float pixelDis, float strength = 1)
        {
            if (strength != 1)
                SetZoomStrength(strength);

            if (ZoomCamera != null)
            {
                Vector3 dir = outPos - inPos;
                float len = dir.magnitude;
                dir = dir.normalized;
                dir *= (pixelDis / Screen.height * this.zoomStrength * dir.magnitude);
                dir = ZoomCamera.transform.position + dir;
                dir.Set(Mathf.Clamp(dir.x, Mathf.Min(inPos.x, outPos.x), Mathf.Max(inPos.x, outPos.x)),
                    Mathf.Clamp(dir.y, Mathf.Min(inPos.y, outPos.y), Mathf.Max(inPos.y, outPos.y)),
                    Mathf.Clamp(dir.z, Mathf.Min(inPos.z, outPos.z), Mathf.Max(inPos.z, outPos.z)));

                ZoomCamera.transform.position = dir;
                zoomCamera.transform.rotation =
                    Quaternion.Lerp(zoomInRot, zoomOutRot, (dir - inPos).magnitude / len);
            }
        }
    }
}
