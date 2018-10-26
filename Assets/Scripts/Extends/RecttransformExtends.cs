using UnityEngine;

public static class RecttransformExtends
{
    public static Vector3 GetBottomLeftLocalPos(
        this RectTransform rectTrans)
    {
        Vector3 pos = rectTrans.localPosition;
        pos.x = pos.x - rectTrans.GetSelfWidth() / 2;
        pos.y = pos.y - rectTrans.GetSelfHeight() / 2;
        pos = rectTrans.LocalPosToCenterPos(pos);

        return pos;
    }

    public static Vector3 GetTopRightLocalPos(
        this RectTransform rectTrans)
    {
        Vector3 pos = rectTrans.localPosition;
        pos.x = pos.x + rectTrans.GetSelfWidth() / 2;
        pos.y = pos.y + rectTrans.GetSelfHeight() / 2;
        pos = rectTrans.LocalPosToCenterPos(pos);

        return pos;
    }

    public static Vector3 LocalPosToCenterPos(
        this RectTransform rectTrans, Vector3? localPos = null)
    {
        localPos = localPos ?? rectTrans.localPosition;
        Vector3 result = new Vector3(rectTrans.pivot.x - 0.5f,
            rectTrans.pivot.y - 0.5f, 0);

        result.Set(result.x * rectTrans.GetSelfWidth(),
            result.y * rectTrans.GetSelfHeight(), result.z);

        result.Set(localPos.Value.x - result.x,
            localPos.Value.y - result.y, result.z);

        return result;
    }

    public static Vector3 CenterPosToLocalPos(
        this RectTransform rectTrans, Vector3 centerPos)
    {
        Vector3 result = new Vector3(rectTrans.pivot.x - 0.5f,
            rectTrans.pivot.y - 0.5f, 0);

        result.Set(result.x * rectTrans.GetSelfWidth(),
            result.y * rectTrans.GetSelfHeight(), result.z);

        result.Set(centerPos.x + result.x,
            centerPos.y + result.y, result.z);

        return result;
    }

    public static Vector3 InversedTransformPointToAnchoredPoint(
        this RectTransform rectTrans, Vector3 localPos)
    {
        Vector2 anchorCenter = GetRelativeAnchor(rectTrans);
        localPos.x = localPos.x - anchorCenter.x *
            GetParentWidth(rectTrans);
        localPos.y = localPos.y - anchorCenter.y *
            GetParentHeight(rectTrans);

        return localPos;
    }

    public static Vector3 AdjustAnchoredPosition(
        this RectTransform rectTrans, Vector3 localPos)
    {
        localPos.x = localPos.x + (rectTrans.pivot.x - 0.5f)
            * GetSelfWidth(rectTrans);
        localPos.y = localPos.y + (rectTrans.pivot.y - 0.5f)
            * GetSelfHeight(rectTrans);
        return localPos;
    }

    public static Vector2 GetRelativeAnchor(this RectTransform rect)
    {
        return new Vector2(rect.anchorMin.x + (rect.anchorMax.x
            - rect.anchorMin.x) * rect.pivot.x,
            rect.anchorMin.y + (rect.anchorMax.y - rect.anchorMin.y)
            * rect.pivot.y);
    }

    public static Vector2 GetReferenceAnchor(this RectTransform rect)
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

    public static float GetSelfWidth(this RectTransform rectTrans)
    {
        return rectTrans.rect.width;
    }

    public static float GetSelfHeight(this RectTransform rectTrans)
    {
        return rectTrans.rect.height;
    }
}
