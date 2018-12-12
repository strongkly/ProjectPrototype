using UnityEngine;
using UnityEngine.UI;
using CrazyBox.Components;

public class TestSprite : MonoBehaviour
{
    public Image img;
    RelativeFix fix;

    void Start()
    {
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
    }
}
