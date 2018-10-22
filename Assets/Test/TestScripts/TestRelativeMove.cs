using UnityEngine;
using CrazyBox.Components;

public class TestRelativeMove : MonoBehaviour {

    void OnGUI()
    {
        if (GUILayout.Button("调整位置"))
        {
            GetComponent<Relatee>().UpdatePos();
        }    
    }
}
