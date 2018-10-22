using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TestRelativeFix : MonoBehaviour {

    void Start()
    {
        DragHandler.Get(gameObject).OnDragAction = OnDrag;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.lossyScale;
        pos.Set(pos.x + ped.delta.x * (1 / scale.x),
            pos.y + ped.delta.y * (1 / scale.y), pos.z);
        transform.localPosition = pos;
    }

    float x = 0;

    void OnGUI()
    {
        GUILayout.Label((transform as RectTransform).anchoredPosition.ToString());
        if (GUILayout.Button("调整位置"))
        {
            GetComponent<RelativeFix>().FixPos();
        }
        x = Convert.ToInt32(GUILayout.TextField(x.ToString()));
        if (GUILayout.Button("设置x 值"))
        {
            (transform as RectTransform).anchoredPosition = new Vector2(x, 0);
        }
    }
}
