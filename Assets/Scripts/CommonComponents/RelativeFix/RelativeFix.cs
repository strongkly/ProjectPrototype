using UnityEngine;

/*                                            notice
 * the setting for both parents of re-locating and relative recttransform has to be the same, which means their 
 * parent transforms are overlapping.
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

        public Vector3 FixPos(RectTransform relatee = null)
        {
            this.relatee = relatee ?? this.relatee;
            Vector3 wLTPoint = new Vector3(),
                wRBPoint = new Vector3();
            Vector3 wRLTPoint = new Vector3(),
                wRRBPoint = new Vector3();

            wLTPoint = selfRectrans.GetTopLeftPosInParentWithFreeAnchor();
            wRBPoint = selfRectrans.GetLowRightPosInParentWithFreeAnchor();
            wLTPoint = transform.parent.TransformPoint(wLTPoint);
            wRBPoint = transform.parent.TransformPoint(wRBPoint);

            wRLTPoint = this.relatee.GetTopLeftPosInParentWithFreeAnchor();
            wRRBPoint = this.relatee.GetLowRightPosInParentWithFreeAnchor();
            wRLTPoint = this.relatee.parent.TransformPoint(wRLTPoint);
            wRRBPoint = this.relatee.parent.TransformPoint(wRRBPoint);

            Vector3 result = new Vector3((wLTPoint.x + wRBPoint.x) / 2,
                (wLTPoint.y + wRBPoint.y) / 2, transform.position.z);

            if (wLTPoint.x < wRLTPoint.x) //左超框
                result.x = wRLTPoint.x + (wRBPoint.x - wLTPoint.x) / 2;
            else if (wRBPoint.x > wRRBPoint.x) //右超框
                result.x = wRRBPoint.x - (wRBPoint.x - wLTPoint.x) / 2;

            if (wLTPoint.y > wRLTPoint.y) //上超框
                result.y = wRLTPoint.y - (wLTPoint.y - wRBPoint.y) / 2;
            else if (wRBPoint.y < wRRBPoint.y) //下超框
                result.y = wRRBPoint.y + (wLTPoint.y - wRBPoint.y) / 2;

            result = transform.parent.InverseTransformPoint(result);
            result = selfRectrans.InversedTransformPointToAnchoredPoint(result);
            result = selfRectrans.AdjustAnchoredPosition(result);

            selfRectrans.anchoredPosition = result;
            return result;
        }
    }
}
