using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Components;

public class CircleFixTest : MonoBehaviour
{
    public RectTransform circleRect;
    public Transform singleTransform;
    public GameObject GroupItemObj;

    void OnGUI()
    {
        if (GUILayout.Button("fixbyHeight"))
        {
            for (int i = 0; i < singleTransform.childCount; i++)
            {
                singleTransform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect);
            }
        }
        if (GUILayout.Button("fixbyWidth"))
        {
            for (int i = 0; i < singleTransform.childCount; i++)
            {
                singleTransform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect, CircleFix.CircleFixType.FixByWidth);
            }
        }
        if (GUILayout.Button("fixbyRadius"))
        {
            for (int i = 0; i < singleTransform.childCount; i++)
            {
                singleTransform.GetChild(i).GetComponent<CircleFix>().FixPos(circleRect, CircleFix.CircleFixType.FixByRadius);
            }
        }
        if (GUILayout.Button("CreateNewGroupItem"))
        {
            GameObject.Instantiate(GroupItemObj, GroupItemObj.transform.parent);
        }
    }
}
