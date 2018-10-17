using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Systems;

public class Relatee : MonoBehaviour
{
    RectTransform rectTransform;

	void Start ()
    {
        rectTransform = transform as RectTransform;
        EventsManager.Instance.AddListener(Relator.evtChangeRelateePos, UpdatePos);
	}

    public void UpdatePos()
    {
        Vector2 relatedPos;
        relatedPos.x = PlayerPrefs.GetFloat("relatedx");
        relatedPos.y = PlayerPrefs.GetFloat("relatedy");

        relatedPos.x *= rectTransform.sizeDelta.x;
        relatedPos.y *= rectTransform.sizeDelta.y;

        rectTransform.anchoredPosition = relatedPos;
    }
}
