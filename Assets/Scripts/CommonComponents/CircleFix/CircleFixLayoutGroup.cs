using UnityEngine;
using UnityEngine.UI;
using CrazyBox.Components;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CrazyBox.Components
{
    public class CircleFixLayoutGroup : VerticalLayoutGroup
    {
        [SerializeField]
        public RectTransform CircleRect;

        protected CircleFixLayoutGroup()
        {
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutVertical();
            if (CircleRect != null)
            {
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform child = rectChildren[i];
                    CircleFix cf = child.GetComponent<CircleFix>();
                    child.localPosition = new Vector3(cf.GetFixedPos(CircleRect).x,
                        child.localPosition.y, child.localPosition.z);
                }
            }
            else
                base.SetLayoutHorizontal();
        }

        /// <summary>
        /// use SetLayoutHorizontal instead
        /// </summary>
        public override void SetLayoutVertical()
        {
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(CircleFixLayoutGroup), true)]
[CanEditMultipleObjects]
public class CircleFixLayoutGroupEditor : Editor
{
    SerializedProperty m_Padding;
    SerializedProperty m_Spacing;
    SerializedProperty m_ChildControlWidth;
    SerializedProperty m_ChildControlHeight;
    SerializedProperty m_ChildForceExpandWidth;
    SerializedProperty m_ChildForceExpandHeight;
    SerializedProperty CircleRect;

    protected virtual void OnEnable()
    {
        m_Padding = serializedObject.FindProperty("m_Padding");
        m_Spacing = serializedObject.FindProperty("m_Spacing");
        m_ChildControlWidth = serializedObject.FindProperty("m_ChildControlWidth");
        m_ChildControlHeight = serializedObject.FindProperty("m_ChildControlHeight");
        m_ChildForceExpandWidth = serializedObject.FindProperty("m_ChildForceExpandWidth");
        m_ChildForceExpandHeight = serializedObject.FindProperty("m_ChildForceExpandHeight");
        CircleRect = serializedObject.FindProperty("CircleRect");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_Padding, true);
        EditorGUILayout.PropertyField(m_Spacing, true);
        EditorGUILayout.PropertyField(CircleRect);

        Rect rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.PrefixLabel(rect, -1, new GUIContent("Child Controls Size"));
        rect.width = Mathf.Max(50, (rect.width - 4) / 3);
        EditorGUIUtility.labelWidth = 50;
        ToggleLeft(rect, m_ChildControlWidth, new GUIContent("Width"));
        rect.x += rect.width + 2;
        ToggleLeft(rect, m_ChildControlHeight, new GUIContent("Height"));
        EditorGUIUtility.labelWidth = 0;

        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.PrefixLabel(rect, -1, new GUIContent("Child Force Expand"));
        rect.width = Mathf.Max(50, (rect.width - 4) / 3);
        EditorGUIUtility.labelWidth = 50;
        ToggleLeft(rect, m_ChildForceExpandWidth, new GUIContent("Width"));
        rect.x += rect.width + 2;
        ToggleLeft(rect, m_ChildForceExpandHeight, new GUIContent("Height"));
        EditorGUIUtility.labelWidth = 0;


        serializedObject.ApplyModifiedProperties();
    }

    void ToggleLeft(Rect position, SerializedProperty property, GUIContent label)
    {
        bool toggle = property.boolValue;
        EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
        EditorGUI.BeginChangeCheck();
        int oldIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        toggle = EditorGUI.ToggleLeft(position, label, toggle);
        EditorGUI.indentLevel = oldIndent;
        if (EditorGUI.EndChangeCheck())
        {
            property.boolValue = property.hasMultipleDifferentValues ? true : !property.boolValue;
        }
        EditorGUI.showMixedValue = false;
    }
}

#endif
