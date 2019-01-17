using UnityEngine;

public static class CameraExtends
{
    public static Vector3 ScreenToWorldPointInRectTransform(
        this Camera camera, Vector2 screenPos, RectTransform rect)
    {
        Vector3 result = Vector3.zero;
        if (!camera.orthographic)
            result = ScreenToWorldPointWithRay(camera, screenPos, rect);
        else
        {
            result.z = rect.position.z - camera.transform.position.z;
            result = camera.ScreenToWorldPoint(screenPos);
        }
        return result;
    }

    /// <summary>
    /// equals to <see cref="RectTransformUtility.ScreenPointToWorldPointInRectangle"/>
    /// </summary>
    /// ScreenPointToRay:cam.ScreenPointToRay explaination:http://www.cs.princeton.edu/courses/archive/fall00/cs426/lectures/raycast/sld008.htm
    public static Vector3 ScreenToWorldPointWithRay(
        this Camera camera, Vector2 screenPos, RectTransform rect)
    {
        Vector3 worldPoint = Vector3.zero;
        Ray ray = RectTransformUtility.ScreenPointToRay(camera, screenPos);
        float enter;
        if (new Plane(rect.rotation * Vector3.back, rect.position).Raycast(ray, out enter))
        {
            worldPoint = ray.GetPoint(enter);
        }
        return worldPoint;
    }
}
