using UnityEngine;
using CrazyBox.Systems;
using UnityEngine.EventSystems;

public class TestRelativeLocation : MonoBehaviour {

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

    void OnGUI()
    {
        if (GUILayout.Button("放在左边"))
        {
            GetComponent<RelativeLocation>().FixLocation(direction: RelativeLocationDirection.left);
        }
        if (GUILayout.Button("放在右边"))
        {
            GetComponent<RelativeLocation>().FixLocation(direction: RelativeLocationDirection.right);
        }
        if (GUILayout.Button("放在中间"))
        {
            GetComponent<RelativeLocation>().FixLocation(direction: RelativeLocationDirection.center);
        }
        if (GUILayout.Button("放在上边"))
        {
            GetComponent<RelativeLocation>().FixLocation(direction: RelativeLocationDirection.top);
        }
        if (GUILayout.Button("放在下边"))
        {
            GetComponent<RelativeLocation>().FixLocation(direction: RelativeLocationDirection.bottom);
        }
    }
}
