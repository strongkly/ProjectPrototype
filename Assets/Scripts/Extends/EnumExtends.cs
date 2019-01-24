using System;
using System.Reflection;

public static class EnumExtends
{
    public static string GetRemark(this Enum value)
    {
        string result;
        Type type = value.GetType();
        FieldInfo field = type.GetField(value.ToString());
        if (field.IsDefined(typeof(EnumRemarkAttributes), true))
        {
            EnumRemarkAttributes attribute = (EnumRemarkAttributes)field.GetCustomAttribute(
                typeof(EnumRemarkAttributes), true);
            result = attribute.GetRemark();
        }
        else
        {
            result = value.ToString();
        }
        return result;
    }
}
