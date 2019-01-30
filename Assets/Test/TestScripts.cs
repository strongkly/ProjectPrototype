using UnityEngine;
using UnityEditor;
using CrazyBox.Components.Functional;
using CrazyBox.Tools;
using CrazyBox.Systems;
using UnityEngine.UI;

public class TestScripts : MonoBehaviour
{
    public Transform targetModel;

    private void OnGUI()
    {
        
    }

    private void OnEnable()
    {
        gameObject.AddComponent<ModelTRSControlPanel>().SetTargetModel(targetModel);
    }

    private void OnDisable()
    {
    }
}

public class TestWindow : EditorWindow
{
    enum eProperty
    {
        [EnumRemarkAttributes("属性1")]
        property1,
        [EnumRemarkAttributes("属性2")]
        property2,
    }

    eProperty e = eProperty.property2;
    string s = "1";
    string[] ss = new string[] { "1", "2" };

    [MenuItem("window/test")]
    public static void Open()
    {
        TestWindow window = EditorWindow.GetWindow<TestWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        s = EditorGUILayoutUtility.Popup(s, ss);

        e = (eProperty)EditorGUILayoutUtility.RemarkEnumPopup(e);
    }
}