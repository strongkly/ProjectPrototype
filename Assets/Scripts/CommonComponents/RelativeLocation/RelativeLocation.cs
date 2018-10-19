﻿using UnityEngine;

/* 
 * before attaching this script,be notice that the following conditions must be satisfied:
 * 1. the anchor of re-locating recttransform need to be set to the left bottom corner of its parent recttransform
 * 2. both pivots of re-locating and relative recttransform needs to be set to center, which is (0.5, 0.5)
 * 3. the setting for both parents of re-locating and relative recttransform has to be the same, which means their 
 * parent transforms are overlapping.
 * 4. all these settings can be seen visually in TestRelativeLocation
*/
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

    public Vector3 FixLocation(RectTransform relatee = null,
        RelativeLocationDirection direction = RelativeLocationDirection.right)
    {
        this.relatee = relatee ?? this.relatee;
        Vector3 result = this.relatee.anchoredPosition;
        result = GetRelateeSourcePos(direction);
        result = relateeParentTrans.TransformPoint(result);

        result = transform.parent.InverseTransformPoint(result);
        AdjustRelatorPos(ref result, direction);
        selfRectrans.anchoredPosition = result;
        
        return result;
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

    #region relatee parent pos 
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

    void AdjustRightPos(ref Vector3 localPos)
    {
        localPos.x = localPos.x - selfRectrans.anchorMin.x * 
            GetRelateeParentWidth() + GetSelfWidth() / 2;
    }

    void AdjustLeftPos(ref Vector3 localPos)
    {
        localPos.x = localPos.x + selfRectrans.anchorMin.x *
            GetParentWidth() - GetSelfWidth() / 2;
    }

    void AdjustCenterPos(ref Vector3 localPos)
    {
        
    }

    void AdjustTopPos(ref Vector3 localPos)
    {
        localPos.y = localPos.y - selfRectrans.anchorMin.y *
            GetRelateeParentHeight() + GetSelfHeight() / 2;
    }

    void AdjustBottomPos(ref Vector3 localPos)
    {
        localPos.y = localPos.y - selfRectrans.anchorMin.y *
            GetRelateeParentWidth() - GetSelfHeight() / 2;
    }

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
        return selfRectrans.sizeDelta.x;
    }

    float GetSelfHeight()
    {
        return selfRectrans.sizeDelta.y;
    }
}
