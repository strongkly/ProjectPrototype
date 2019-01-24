using System;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumRemarkAttributes : Attribute
{
    string remarkStr = null;

    public EnumRemarkAttributes(string remarkStr)
    {
        this.remarkStr = remarkStr;
    }

    public string GetRemark()
    {
        return remarkStr;
    }
}
