using UnityEngine;
using CrazyBox.Systems;
using UnityEngine.EventSystems;

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

    void OnGUI()
    {
        if (GUILayout.Button("调整位置"))
        {
            EventsManager.Instance.Dispatch(RelativeFix.evtFixPos);
        }    
    }
}
