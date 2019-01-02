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

    /// <summary>
    /// change position from pivot related position to visual center related position
    /// </summary>
    /// <param name="localPos"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Inverse to LocalPosToCenterPos, which change position from visual center related position to pivot related position
    /// </summary>
    /// <param name="centerPos"></param>
    /// <returns></returns>
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

    public static float GetSelfWidth(this RectTransform rectTrans)
    {
        return rectTrans.rect.width;
    }

    public static float GetSelfHeight(this RectTransform rectTrans)
    {
        return rectTrans.rect.height;
    }

    public static bool IsWorldPosInRect(this RectTransform rectTrans, Vector2 worldPos)
    {
        bool result = true;

        Vector3[] rectPoints = new Vector3[4];
        rectTrans.GetWorldCorners(rectPoints);

        if (worldPos.x < rectPoints[0].x ||
            worldPos.x > rectPoints[2].x)
            result = false;
        else if (worldPos.y < rectPoints[0].y || 
            worldPos.y > rectPoints[2].y)
            result = false;

        return result;
    }
}
