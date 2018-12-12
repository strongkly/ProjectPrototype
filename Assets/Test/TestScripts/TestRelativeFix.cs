using UnityEngine;
using UnityEngine.UI;
using CrazyBox.Components;
using UnityEngine.EventSystems;

public class TestRelativeFix : MonoBehaviour {

    public Image img;
    public GameObject relocatingObj;
    RelativeFix fix;

    void Start()
    {
        DragHandler.Get(relocatingObj).OnDragAction = OnDrag;

        #region rejectfix
        fix = RelativeFix.Get(img.transform);
        DragHandler dh = DragHandler.Get(img.gameObject);
        dh.OnEndAction = (ped) =>
        {
            fix.RejectFixPos(img.transform.parent as RectTransform);
        };
        dh.OnDragAction = (ped) =>
        {
            //mapContainer.transform.localPosition += ped.delta
            Vector2 pos = img.transform.localPosition;
            pos += ped.delta;
            img.transform.localPosition = pos;
        };
        #endregion
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector3 pos = relocatingObj.transform.localPosition;
        Vector3 scale = relocatingObj.transform.lossyScale;
        pos.Set(pos.x + ped.delta.x * (1 / scale.x),
            pos.y + ped.delta.y * (1 / scale.y), pos.z);
        relocatingObj.transform.localPosition = pos;
    }

    void OnGUI()
    {
        GUILayout.Label((relocatingObj.transform as RectTransform).
            anchoredPosition.ToString());
        if (GUILayout.Button("调整位置"))
        {
            relocatingObj.GetComponent<RelativeFix>().LimitFixPos();
        }
    }
}
