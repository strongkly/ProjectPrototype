using CrazyBox.Systems;
using UnityEngine;

public class RelativeLocation : MonoBehaviour {

    [SerializeField]
    public RectTransform relatee;

    RectTransform selfRectrans
    {
        get
        {
            return transform as RectTransform;
        }
    }
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

    public Vector3 FixLocation(RectTransform relatee = null)
    {
        this.relatee = relatee ?? this.relatee;
        Vector3 result = this.relatee.anchoredPosition;
        result.x = this.relatee.anchoredPosition.x + relateeSize.x / 2;
        result = this.relatee.parent.TransformPoint(result);

        result = transform.parent.InverseTransformPoint(result);
        result.x = result.x + selfSize.x / 2;
        selfRectrans.anchoredPosition = result;
        
        return result;
    }


}
