using System;
using System.Reflection;

/// <summary>
/// Unity 使用的 .net 2.0 缺失部分API 
/// </summary>
public static class UnityCSharpPatch
{
    public static T GetCustomAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute
    {
        T result = default(T);
        object[] attrs = member.GetCustomAttributes(typeof(T), inherit);
        if (attrs != null && attrs.Length > 0)
            result = attrs[0] as T;
        return result;
    }

    public static object GetCustomAttribute(this MemberInfo member, Type attributeType, bool inherit)
    {
        object result = null;
        object[] attrs = member.GetCustomAttributes(attributeType, inherit);
        if (attrs != null && attrs.Length > 0)
            result = attrs[0];
        return result;
    }
}
