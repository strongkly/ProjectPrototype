using UnityEngine;
using UnityEngine.EventSystems;
using CrazyBox.Systems;

public class Relator : MonoBehaviour
{
    RectTransform rectTransform
    {
        get
        {
            return transform as RectTransform;
        }
    }

    void Start ()
    {
        DragHandler.Get(gameObject).OnDragAction = OnDrag;
        DragHandler.Get(gameObject).OnDropAction = OnDrop;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.lossyScale;
        pos.Set(pos.x + ped.delta.x * (1 /scale.x),
            pos.y + ped.delta.y * (1 / scale.y), pos.z);
        transform.localPosition = pos;
    }

    public void OnDrop(PointerEventData ped)
    {
        Vector3 pos = rectTransform.anchoredPosition;
        PlayerPrefs.SetFloat("relatedx", pos.x / rectTransform.sizeDelta.x);
        PlayerPrefs.SetFloat("relatedy", pos.y / rectTransform.sizeDelta.y);
    }
}
