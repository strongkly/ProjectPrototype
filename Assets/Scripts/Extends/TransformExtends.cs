using UnityEngine;

public static class TransformExtends
{
    public static bool TryFindTransformFromDescendantsByName(this Transform parent, 
        ref TransformExtends findTrans, string name)
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
                    if (TryFindTransformInChildByName(parent.GetChild(i), ref findTrans, name))
                    {
                        result = true;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
