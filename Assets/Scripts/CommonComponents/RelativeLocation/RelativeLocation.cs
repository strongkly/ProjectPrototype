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

        /// <summary>
        /// Rely on parent width and height,be sure both parent of re-location an relatee RectTransform are 
        /// set to the same size
        /// </summary>
        /// <param name="relatee"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 FixAnchoredPosition(RectTransform relatee = null,
            RelativeLocationDirection direction = RelativeLocationDirection.right)
        {
            this.relatee = relatee ?? this.relatee;
            Vector3 result = this.relatee.anchoredPosition;
            result = GetRelateeSourcePos(direction);
            result = relateeParentTrans.TransformPoint(result);

            result = transform.parent.InverseTransformPoint(result);
            AdjustRelatorPos(ref result, direction);
            result = selfRectrans.AdjustAnchoredPosition(result);
            selfRectrans.anchoredPosition = result;

            return result;
        }

        /// <summary>
        /// Be sure both pivots of re-location and relatee RectTransform are set to center, which is (0.5, 0.5)
        /// </summary>
        /// <param name="relatee"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 FixLocalPosition(RectTransform relatee = null,
            RelativeLocationDirection direction = RelativeLocationDirection.right)
        {
            this.relatee = relatee ?? this.relatee;
            Vector3 result = this.relatee.localPosition;
            result = GetRelateeLocalPos(direction);
            result = this.relatee.parent.TransformPoint(result);

            result = transform.parent.InverseTransformPoint(result);
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

        Vector3 GetRelateeSourcePos(RelativeLocationDirection direction =
            RelativeLocationDirection.right)
        {
            Vector3 result;
            switch (direction)
            {
                case RelativeLocationDirection.left:
                    result = GetLeftPosInFreeAnchor();
                    break;
                case RelativeLocationDirection.right:
                    result = GetRightPosInFreeAnchor();
                    break;
                case RelativeLocationDirection.center:
                    result = GetCenterPosInFreeAnchor();
                    break;
                case RelativeLocationDirection.top:
                    result = GetTopPosInFreeAnchor();
                    break;
                case RelativeLocationDirection.bottom:
                    result = GetBottomPosInFreeAnchor();
                    break;
                default:
                    result = Vector3.zero;
                    break;
            }
            return result;
        }

        void AdjustRelatorPos(ref Vector3 original, RelativeLocationDirection direction =
            RelativeLocationDirection.right)
        {
            switch (direction)
            {
                case RelativeLocationDirection.left:
                    AdjustLeftPos(ref original);
                    break;
                case RelativeLocationDirection.right:
                    AdjustRightPos(ref original);
                    break;
                case RelativeLocationDirection.center:
                    AdjustCenterPos(ref original);
                    break;
                case RelativeLocationDirection.top:
                    AdjustTopPos(ref original);
                    break;
                case RelativeLocationDirection.bottom:
                    AdjustBottomPos(ref original);
                    break;
                default:
                    break;
            }
        }

        #region relatee pos 
        Vector3 GetRightPosInFreeAnchor()
        {
            Vector3 result = Vector3.zero;
            result.x = relatee.anchorMin.x * GetRelateeParentWidth() +
                relatee.offsetMin.x + GetRelateeWidth();
            result.y = relatee.anchorMin.y * GetRelateeParentHeight() +
                relatee.offsetMin.y + GetRelateeHeight() / 2;

            return result;
        }

        Vector3 GetLeftPosInFreeAnchor()
        {
            Vector3 result = Vector3.zero;
            result.x = relatee.anchorMin.x * GetRelateeParentWidth() +
                relatee.offsetMin.x;
            result.y = relatee.anchorMin.y * GetRelateeParentHeight() +
                relatee.offsetMin.y + GetRelateeHeight() / 2;

            return result;
        }

        Vector3 GetCenterPosInFreeAnchor()
        {
            Vector3 result = Vector3.zero;
            result.x = relatee.anchorMin.x * GetRelateeParentWidth() +
                relatee.offsetMin.x + GetRelateeWidth() / 2;
            result.y = relatee.anchorMin.y * GetRelateeParentHeight() +
                relatee.offsetMin.y + GetRelateeHeight() / 2;

            return result;
        }

        Vector3 GetTopPosInFreeAnchor()
        {
            Vector3 result = Vector3.zero;
            result.x = relatee.anchorMin.x * GetRelateeParentWidth() +
                relatee.offsetMin.x + GetRelateeWidth() / 2;
            result.y = relatee.anchorMin.y * GetRelateeParentHeight() +
                relatee.offsetMin.y + GetRelateeHeight();

            return result;
        }

        Vector3 GetBottomPosInFreeAnchor()
        {
            Vector3 result = Vector3.zero;
            result.x = relatee.anchorMin.x * GetRelateeParentWidth() +
                relatee.offsetMin.x + GetRelateeWidth() / 2;
            result.y = relatee.anchorMin.y * GetRelateeParentHeight() +
                relatee.offsetMin.y;

            return result;
        }
        #endregion

        #region relocation pos
        void AdjustRightPos(ref Vector3 localPos)
        {
            Vector2 anchorCenter = selfRectrans.GetRelativeAnchor(); //GetAnchorCenter(selfRectrans);
            localPos.x = localPos.x - anchorCenter.x *
                GetParentWidth() + GetSelfWidth() / 2;
            localPos.y = localPos.y - anchorCenter.y *
                GetParentHeight();
        }

        void AdjustLeftPos(ref Vector3 localPos)
        {
            Vector2 anchorCenter = selfRectrans.GetRelativeAnchor();//GetAnchorCenter(selfRectrans);
            localPos.x = localPos.x - anchorCenter.x *
                GetParentWidth() - GetSelfWidth() / 2;
            localPos.y = localPos.y - anchorCenter.y *
                GetParentHeight();
        }

        void AdjustCenterPos(ref Vector3 localPos)
        {
            localPos = selfRectrans.InversedTransformPointToAnchoredPoint(localPos);
        }

        void AdjustTopPos(ref Vector3 localPos)
        {
            Vector2 anchorCenter = selfRectrans.GetRelativeAnchor();
            localPos.x = localPos.x - anchorCenter.x *
                GetParentWidth();
            localPos.y = localPos.y - anchorCenter.y *
                GetParentHeight() + GetSelfHeight() / 2;
        }

        void AdjustBottomPos(ref Vector3 localPos)
        {
            Vector2 anchorCenter = selfRectrans.GetRelativeAnchor();
            localPos.x = localPos.x - anchorCenter.x *
                GetParentWidth();
            localPos.y = localPos.y - anchorCenter.y *
                GetParentHeight() - GetSelfHeight() / 2;
        }
        #endregion

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