using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Components;

public class CircleFixTest : MonoBehaviour
{
    public RectTransform circleRect;

    void OnGUI()
    {
        if (GUILayout.Button("fixbyHeight"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect);
            }
        }
        if (GUILayout.Button("fixbyWidth"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect, CircleFix.CircleFixType.FixByWidth);
            }
        }
        if (GUILayout.Button("fixbyRadius"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect, CircleFix.CircleFixType.FixByRadius);
            }
        }
    }
}
