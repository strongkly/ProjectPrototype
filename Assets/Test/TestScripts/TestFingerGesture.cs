using UnityEngine;
using UnityEngine.UI;
using CrazyBox.Systems;

public class TestFingerGesture : MonoBehaviour
{
    public Image img;
    public Text text;

    Vector2 originalSize;

    void Start()
    {
        FingerGesture.Instance.IsOn = true;
        FingerGesture.Instance.SetZoomInCallBack(Zoom);
        FingerGesture.Instance.SetZoomOutCallBack(Zoom);

        originalSize = img.rectTransform.rect.size;
    }

    void Zoom(float delta)
    {
        Vector2 size = img.rectTransform.sizeDelta;
        #if !UNITY_EDITOR
        delta = delta / Screen.height * 5f;
        #endif
        size.Set(size.x + delta * size.x, size.y + delta * size.y);
        img.rectTransform.sizeDelta = size;
        text.text = size.ToString();
    }
}
