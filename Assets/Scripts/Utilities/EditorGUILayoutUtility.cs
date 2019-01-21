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
}
