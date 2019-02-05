using UnityEngine;
using CrazyBox.Components;

public class TestCrossValueSetter : MonoBehaviour
{
    CrossValueSetter cvs;

    void Start()
    {
        cvs = transform.GetChild(0).GetComponent<CrossValueSetter>();
        cvs.onValueChange.AddListener(OnValueChange);
    }

    void OnValueChange(Vector2 value)
    {
        Debug.LogErrorFormat("x:{0}  y:{1}", value.x, value.y);
    }
}
