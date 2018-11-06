using UnityEngine;

public class TestAnchorPos : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.Label((transform as RectTransform).localPosition.ToString());
        GUILayout.Label(GetCenterPosition().ToString());
    }

    RectTransform selfRect
    {
        get
        {
            return transform as RectTransform;
        }
    }

    Vector2 GetCenterPosition()
    {
        Vector2 pos = Vector2.zero;
        pos.x = transform.localPosition.x;
        pos.y = transform.localPosition.y;

        pos = selfRect.LocalPosToCenterPos(pos);

        return pos;
    }
}
