using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CrazyBox.Components.Functional;
using System;

public class TestMonsterBloodBar : MonoBehaviour
{
    public Transform mbp;

    string layer = "20", value = "0.5";
    void OnGUI()
    {
        layer = GUILayout.TextField(layer);
        value = GUILayout.TextField(value);

        if (GUILayout.Button("设置（无动画）"))
        {
            mbp.GetComponent<MonsterBloodProgressbar>().SetValue(
                Convert.ToInt32(layer), (float)Convert.ToDouble(value));
        }
        if (GUILayout.Button("设置（有动画）"))
        {
            mbp.GetComponent<MonsterBloodProgressbar>().SetValueWithAnimation(
                Convert.ToInt32(layer), (float)Convert.ToDouble(value));
        }
    }
}
