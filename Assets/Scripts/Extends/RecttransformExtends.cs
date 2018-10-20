using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecttransformExtends
{
    public static Vector3 GetLowLeftPosInParentWithFreeAnchor(
        this RectTransform rectTrans)
    {
        Vector3 result = Vector3.zero;
        result.x = rectTrans.anchorMin.x * GetParentWidth(rectTrans) +
            rectTrans.offsetMin.x;
        result.y = rectTrans.anchorMin.y * GetParentHeight(rectTrans) +
            rectTrans.offsetMin.y;

        return result;
    }

    public static Vector3 GetLowRightPosInParentWithFreeAnchor(
        this RectTransform rectTrans)
    {
        Vector3 result = Vector3.zero;
        result.x = rectTrans.anchorMax.x * GetParentWidth(rectTrans) +
            rectTrans.offsetMax.x;
        result.y = rectTrans.anchorMin.y * GetParentHeight(rectTrans) +
            rectTrans.offsetMin.y;

        return result;
    }

    public static Vector3 GetTopLeftPosInParentWithFreeAnchor(
        this RectTransform rectTrans)
    {
        Vector3 result = Vector3.zero;
        result.x = rectTrans.anchorMin.x * GetParentWidth(rectTrans) +
            rectTrans.offsetMin.x;
        result.y = rectTrans.anchorMax.y * GetParentHeight(rectTrans) +
            rectTrans.offsetMax.y;

        return result;
    }

    public static Vector3 GetTopRightPosInParentWithFreeAnchor(
        this RectTransform rectTrans)
    {
        Vector3 result = Vector3.zero;
        result.x = rectTrans.anchorMax.x * GetParentWidth(rectTrans) +
            rectTrans.offsetMax.x;
        result.y = rectTrans.anchorMax.y * GetParentHeight(rectTrans) +
            rectTrans.offsetMax.y;

        return result;
    }

    public static Vector3 AdjustPosInParentWithFreeAnchor(
        this RectTransform rectTrans, Vector3 localPos)
    {
        Vector2 anchorCenter = GetAnchorCenter(rectTrans);
        localPos.x = localPos.x - anchorCenter.x *
            GetParentWidth(rectTrans);
        localPos.y = localPos.y - anchorCenter.y *
            GetParentHeight(rectTrans);

        return localPos;
    }

    public static Vector2 GetAnchorCenter(this RectTransform rect)
    {
        return new Vector2((rect.anchorMin.x + rect.anchorMax.x) / 2,
            (rect.anchorMin.y + rect.anchorMax.y) / 2);
    }

    public static float GetParentWidth(this RectTransform rectTrans)
    {
        float result = 0f;
        if (rectTrans != null)
        {
            result = (rectTrans.parent as RectTransform).rect.width;
        }
        return result;
    }

    public static float GetParentHeight(this RectTransform rectTrans)
    {
        float result = 0f;
        if (rectTrans != null)
        {
            result = (rectTrans.parent as RectTransform).rect.height;
        }
        return result;
    }
}
