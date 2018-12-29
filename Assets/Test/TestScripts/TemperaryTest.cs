using UnityEngine;
using CrazyBox.Tools;
using CrazyBox.Components;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class TemperaryTest : MonoBehaviour
{
    TextureMaskSampler tms;
    // Start is called before the first frame update
    void Start()
    {
        Texture2D t2d = Resources.Load<Texture2D>("TestClickMask");
        ClickHandler.Get(this.gameObject).OnClickAction = CheckIsBlocked;
        tms = transform.GetOrAdd<TextureMaskSampler>();
        tms.Init();
    }

    public void CheckIsBlocked(PointerEventData ped)
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(ped.pressPosition);

        Debug.LogError(tms.IsBlocked(pos));
    }
}
