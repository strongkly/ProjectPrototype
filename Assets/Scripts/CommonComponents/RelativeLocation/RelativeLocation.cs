using UnityEngine;

namespace CrazyBox.Components
{
    public enum RelativeLocationDirection
    {
        left,
        right,
        center,
        top,
        bottom
    }

    public class RelativeLocation : MonoBehaviour {

        [SerializeField]
        public RectTransform relatee;

        RectTransform selfRectrans
        {
            get
            {
                return transform as RectTransform;
            }
        }
        RectTransform relateeParentTrans
        {
            get
            {
                return relatee.parent as RectTransform;
            }
        }
        RectTransform parentTrans
        {
            get
            {
                return transform.parent as RectTransform;
            }
        }

        public Vector3 FixLocalPosition(RectTransform relatee = null,
            RelativeLocationDirection direction = RelativeLocationDirection.right)
        {
            this.relatee = relatee ?? this.relatee;
            Vector3 result = GetRelateeLocalPos(direction);
            result = this.relatee.LocalPosToCenterPos(result);
            result = this.relatee.parent.TransformPoint(result);

            result = transform.parent.InverseTransformPoint(result);
            result = selfRectrans.CenterPosToLocalPos(result);
            result = AdjustRelatorLocalPos(result, direction);
            selfRectrans.localPosition = result;

            return result;
        }

        Vector3 GetRelateeLocalPos(RelativeLocationDirection direction =
            RelativeLocationDirection.right)
        {
            Vector3 delta = Vector3.zero;

            switch (direction)
            {
                case RelativeLocationDirection.left:
                    delta.x -= this.relatee.GetSelfWidth() / 2;
                    break;
                case RelativeLocationDirection.right:
                    delta.x += this.relatee.GetSelfWidth() / 2;
                    break;
                case RelativeLocationDirection.center:
                    break;
                case RelativeLocationDirection.top:
                    delta.y += this.relatee.GetSelfHeight() / 2;
                    break;
                case RelativeLocationDirection.bottom:
                    delta.y -= this.relatee.GetSelfHeight() / 2;
                    break;
                default:
                    break;
            }
            return relatee.localPosition + delta;
        }

        Vector3 AdjustRelatorLocalPos(Vector3 localPos, RelativeLocationDirection
            direction = RelativeLocationDirection.right)
        {
            Vector3 delta = Vector3.zero;

            switch (direction)
            {
                case RelativeLocationDirection.left:
                    delta.x -= selfRectrans.GetSelfWidth() / 2;
                    break;
                case RelativeLocationDirection.right:
                    delta.x += selfRectrans.GetSelfWidth() / 2;
                    break;
                case RelativeLocationDirection.center:
                    break;
                case RelativeLocationDirection.top:
                    delta.y += selfRectrans.GetSelfHeight() / 2;
                    break;
                case RelativeLocationDirection.bottom:
                    delta.y -= selfRectrans.GetSelfHeight() / 2;
                    break;
                default:
                    break;
            }
            return localPos + delta;
        }

        #region Assist func
        float GetRelateeParentWidth()
        {
            return relateeParentTrans.rect.width;
        }

        float GetRelateeParentHeight()
        {
            return relateeParentTrans.rect.height;
        }

        float GetRelateeWidth()
        {
            return relatee.rect.width;
        }

        float GetRelateeHeight()
        {
            return relatee.rect.height;
        }

        float GetParentWidth()
        {
            return parentTrans.rect.width;
        }

        float GetParentHeight()
        {
            return parentTrans.rect.height;
        }

        float GetSelfWidth()
        {
            return selfRectrans.rect.width;
        }

        float GetSelfHeight()
        {
            return selfRectrans.rect.height;
        }
        #endregion
    }
}