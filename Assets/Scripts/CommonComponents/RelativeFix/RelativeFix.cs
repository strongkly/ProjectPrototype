using CrazyBox.Systems;
using UnityEngine;

public class RelativeFix : MonoBehaviour {

    [SerializeField]
    public RectTransform relatee;

    RectTransform selfRectrans;
    Vector2 selfSize
    {
        get
        {
            return selfRectrans.sizeDelta;
        }
    }
    Vector2 relateeSize
    {
        get
        {
            return relatee.sizeDelta;
        }
    }

    void Start()
    {
        selfRectrans = transform as RectTransform;
    }

    public Vector3 FixPos(RectTransform relatee = null)
    {
        this.relatee = relatee ?? this.relatee;
        Vector3 wLTPoint = new Vector3(),
            wRBPoint = new Vector3();
        Vector3 wRLTPoint = new Vector3(),
            wRRBPoint = new Vector3();
        wLTPoint.x = selfRectrans.anchoredPosition.x - selfSize.x / 2;
        wLTPoint.y = selfRectrans.anchoredPosition.y + selfSize.y / 2;
        wRBPoint.x = selfRectrans.anchoredPosition.x + selfSize.x / 2;
        wRBPoint.y = selfRectrans.anchoredPosition.y - selfSize.y / 2;

        wLTPoint = transform.parent.TransformPoint(wLTPoint);
        wRBPoint = transform.parent.TransformPoint(wRBPoint);

        wRLTPoint.x = this.relatee.anchoredPosition.x - relateeSize.x / 2;
        wRLTPoint.y = this.relatee.anchoredPosition.y + relateeSize.y / 2;
        wRRBPoint.x = this.relatee.anchoredPosition.x + relateeSize.x / 2;
        wRRBPoint.y = this.relatee.anchoredPosition.y - relateeSize.y / 2;

        wRLTPoint = this.relatee.parent.TransformPoint(wRLTPoint);
        wRRBPoint = this.relatee.parent.TransformPoint(wRRBPoint);
        Vector3 result = transform.position;

        if (wLTPoint.x < wRLTPoint.x) //左超框
            result.x = wRLTPoint.x + (wRBPoint.x - wLTPoint.x) / 2;
        else if (wRBPoint.x > wRRBPoint.x) //右超框
            result.x = wRRBPoint.x - (wRBPoint.x - wLTPoint.x) / 2;

        if (wLTPoint.y > wRLTPoint.y) //上超框
            result.y = wRLTPoint.y - (wLTPoint.y - wRBPoint.y) / 2;
        else if (wRBPoint.y < wRRBPoint.y) //下超框
            result.y = wRRBPoint.y + (wLTPoint.y - wRBPoint.y) / 2;

        selfRectrans.anchoredPosition = transform.parent.InverseTransformPoint(result);
        return result;
    }


}
