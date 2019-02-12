using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Events;

public sealed class EditorGUILayoutUtility
{
    public static string Popup(string selected, string[] displayOptions,
        UnityAction<string> selectCallBack = null)
    {
        if (selectCallBack != null)
        {
            string result = displayOptions[EditorGUILayout.Popup(
                GetStringIdx(selected, displayOptions), displayOptions)];
            if (result != selected)
            {
                if (selectCallBack != null)
                    selectCallBack.Invoke(result);
            }
            return result;
        }
        else
            return displayOptions[EditorGUILayout.Popup(
                GetStringIdx(selected, displayOptions), displayOptions)];

    }

    public static string Popup(string label, string selected, string[] displayOptions,
        UnityAction<string> selectCallBack = null)
    {
        if (selectCallBack != null)
        {
            string result = displayOptions[EditorGUILayout.Popup(label, 
                GetStringIdx(selected, displayOptions), displayOptions)];
            if (result != selected)
            {
                if (selectCallBack != null)
                    selectCallBack.Invoke(result);
            }
            return result;
        }
        else
            return displayOptions[EditorGUILayout.Popup(label, 
                GetStringIdx(selected, displayOptions), displayOptions)];

    }

    static int GetStringIdx(string selected, string[] displayOptions)
    {
        int result = 0;
        for (int i = 0; i < displayOptions.Length; i++)
        {
            if (displayOptions[i] == selected)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    static Dictionary<Type, List<Enum>> EnumTypeToAllEnumValues = new Dictionary<Type, List<Enum>>();
    static Dictionary<Type, string[]> EnumTypeToAllEnumRemarks = new Dictionary<Type, string[]>();

    public static Enum RemarkEnumPopup(Enum selectEnum)
    {
        return GetEnumByRemarkName(Popup(selectEnum.GetRemark(),
            GetRemarkEnumAllRemarks(selectEnum)), selectEnum);
    }

    public static Enum RemarkEnumPopup(string label, Enum selectEnum)
    {
        return GetEnumByRemarkName(Popup(label, selectEnum.GetRemark(),
           GetRemarkEnumAllRemarks(selectEnum)), selectEnum);
    }

    static string[] GetRemarkEnumAllRemarks(Enum e)
    {
        Type type = e.GetType();
        string[] result;
        if (!EnumTypeToAllEnumRemarks.ContainsKey(type))
        {
            List<Enum> enumAllValue = GetOrAddEnumAllValuesByEnumType(type);
            result = new string[enumAllValue.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = enumAllValue[i].GetRemark();
            }
            EnumTypeToAllEnumRemarks.Add(type, result);
        }
        else
        {
            result = EnumTypeToAllEnumRemarks[type];
        }
        return result;
    }

    static List<Enum> GetOrAddEnumAllValuesByEnumType(Type type)
    {
        if (!EnumTypeToAllEnumValues.ContainsKey(type))
        {
            List<Enum> newEnumList = new List<Enum>();
            Array arr = Enum.GetValues(type);
            foreach (var value in arr)
            {
                newEnumList.Add(value as Enum);
            }
            EnumTypeToAllEnumValues.Add(type, newEnumList);
        }
        return EnumTypeToAllEnumValues[type];
    }

    static Enum GetEnumByRemarkName(string remarkName, Enum e)
    {
        Enum result = null;
        Type type = e.GetType();
        if (EnumTypeToAllEnumValues.ContainsKey(type))
        {
            foreach (Enum en in EnumTypeToAllEnumValues[type])
            {
                if (en.GetRemark() == remarkName)
                {
                    result = en;
                    break;
                }
            }
        }
        return result;
    }
}
