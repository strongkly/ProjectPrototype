using UnityEngine;

public static class TransformExtends
{
    public static bool TryFindTransformFromDescendantsByName(this Transform parent, 
        ref Transform findTrans, string name)
    {
        bool result = false;

        if (parent.childCount != 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).name == name)
                {
                    findTrans = parent.GetChild(i);
                    result = true;
                    break;
                }
            }
            if (!result)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (TryFindTransformFromDescendantsByName(parent.GetChild(i), ref findTrans, name))
                    {
                        result = true;
                        break;
                    }
                }
            }
        }

        return result;
    }

    public static T GetOrAdd<T>(this Transform transform)
        where T : Component
    {
        T result = transform.GetComponent<T>();
        if (result == null)
            result = transform.gameObject.AddComponent<T>();

        return result;
    }
}
