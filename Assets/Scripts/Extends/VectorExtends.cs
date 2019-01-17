using UnityEngine;

public static class VectorExtends
{
    /// <summary>
    /// Rotate this point around pivot with eulerAngles
    /// <para> use <see cref="Transform.RotateAround"/> when tring to rotate a GameObject</para>
    /// </summary>
    public static Vector3 RotateAroundPivot(this Vector3 self, Vector3 pivot, float x, float y, float z)
    {
        Vector3 dir = self - pivot;
        dir = Quaternion.Euler(x, y, z) * dir;
        self = dir + pivot;
        return self;
    }

    /// <summary>
    /// Rotate this point around pivot with eulerAngles
    /// <para> use <see cref="Transform.RotateAround"/> when tring to rotate a GameObject</para>
    /// </summary>
    public static Vector3 RotateAroundPivot(this Vector3 self, Vector3 pivot, Vector3 eulerAngles)
    {
        Vector3 dir = self - pivot;
        dir = Quaternion.Euler(eulerAngles) * dir;
        self = dir + pivot;
        return self;
    }
}
