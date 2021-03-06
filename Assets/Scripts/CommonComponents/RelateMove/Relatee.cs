﻿using UnityEngine;

namespace CrazyBox.Components
{
    public class Relatee : MonoBehaviour
    {
        RectTransform rectTransform
        {
            get
            {
                return transform as RectTransform;
            }
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
}
