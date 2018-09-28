using System.Collections.Generic;

public static class HashSetExtend
{
    public static T ElementAt<T>(this HashSet<T> set, int index)
    {
        T Result = default(T);
        if (index <= set.Count - 1)
        {
            var iter = set.GetEnumerator();
            while (iter.MoveNext())
            {
                if (index == 0)
                {
                    Result = iter.Current;
                    break;
                }
                index--;
            }
        }
        return Result;
    }
}
