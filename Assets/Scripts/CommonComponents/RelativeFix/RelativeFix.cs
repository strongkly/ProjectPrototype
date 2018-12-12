using UnityEngine;

/*                                            notice
 * the size for both parents of re-locating and relative RectTransform has to be the same, which means their 
 * parent transforms could fully overlapping.
 */

namespace CrazyBox.Components
{
    public class RelativeFix : MonoBehaviour
    {
        [SerializeField]
        public RectTransform relatee;

        RectTransform selfRectrans
        {
            get
            {
                return transform as RectTransform;
            }
        }
        Vector2 selfSize
        {
            get
            {
                return selfRectrans.sizeDelta;
            }
        }
        Vector2 relateeSize
        {
            get
            {
                return relatee.sizeDelta;
            }
        }

        public static RelativeFix Get(Transform target)
        {
            RelativeFix result = target.GetComponent<RelativeFix>();
            if (result == null)
                result = target.gameObject.AddComponent<RelativeFix>();

            return result;
        }

        public Vector3 LimitFixPos(RectTransform relatee = null)
        {
            this.relatee = relatee ?? this.relatee;

            Vector3 wLBPoint = selfRectrans.GetBottomLeftLocalPos();
            Vector3 wRTPoint = selfRectrans.GetTopRightLocalPos();
            Vector3 wRLBPoint = this.relatee.GetBottomLeftLocalPos();
            Vector3 wRRTPoint = this.relatee.GetTopRightLocalPos();


            wLBPoint = selfRectrans.parent.TransformPoint(wLBPoint);
            wRTPoint = selfRectrans.parent.TransformPoint(wRTPoint);
            wRLBPoint = this.relatee.parent.TransformPoint(wRLBPoint);
            wRRTPoint = this.relatee.parent.TransformPoint(wRRTPoint);

            Vector3 result = selfRectrans.parent.TransformPoint(
                selfRectrans.LocalPosToCenterPos());

            if (wLBPoint.x < wRLBPoint.x) //左超框
                result.x = wRLBPoint.x + (wRTPoint.x - wLBPoint.x) / 2;
            else if (wRTPoint.x > wRRTPoint.x) //右超框
                result.x = wRRTPoint.x - (wRTPoint.x - wLBPoint.x) / 2;

            if (wRTPoint.y > wRRTPoint.y) //上超框
                result.y = wRRTPoint.y - (wRTPoint.y - wLBPoint.y) / 2;
            else if (wLBPoint.y < wRLBPoint.y) //下超框
                result.y = wRLBPoint.y + (wRTPoint.y - wLBPoint.y) / 2;

            result = selfRectrans.parent.InverseTransformPoint(result);
            result = selfRectrans.CenterPosToLocalPos(result);

            selfRectrans.localPosition = result;
            return result;
        }

        public Vector3 RejectFixPos(RectTransform relatee = null)
        {
            this.relatee = relatee ?? this.relatee;

            Vector3 wLBPoint = selfRectrans.GetBottomLeftLocalPos();
            Vector3 wRTPoint = selfRectrans.GetTopRightLocalPos();
            Vector3 wRLBPoint = this.relatee.GetBottomLeftLocalPos();
            Vector3 wRRTPoint = this.relatee.GetTopRightLocalPos();


            wLBPoint = selfRectrans.parent.TransformPoint(wLBPoint);
            wRTPoint = selfRectrans.parent.TransformPoint(wRTPoint);
            wRLBPoint = this.relatee.parent.TransformPoint(wRLBPoint);
            wRRTPoint = this.relatee.parent.TransformPoint(wRRTPoint);

            Vector3 result = selfRectrans.parent.TransformPoint(
                selfRectrans.LocalPosToCenterPos());

            if (wLBPoint.x > wRLBPoint.x) //左进框
                result.x = wRLBPoint.x + (wRTPoint.x - wLBPoint.x) / 2;
            else if (wRTPoint.x < wRRTPoint.x) //右进框
                result.x = wRRTPoint.x - (wRTPoint.x - wLBPoint.x) / 2;

            if (wRTPoint.y < wRRTPoint.y) //上进框
                result.y = wRRTPoint.y - (wRTPoint.y - wLBPoint.y) / 2;
            else if (wLBPoint.y > wRLBPoint.y) //下进框
                result.y = wRLBPoint.y + (wRTPoint.y - wLBPoint.y) / 2;

            result = selfRectrans.parent.InverseTransformPoint(result);
            result = selfRectrans.CenterPosToLocalPos(result);

            selfRectrans.localPosition = result;
            return result;
        }
    }
}
