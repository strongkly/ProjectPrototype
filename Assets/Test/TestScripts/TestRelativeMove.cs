using UnityEngine;
using CrazyBox.Systems;
using UnityEngine.EventSystems;

public class TestRelativeMove : MonoBehaviour {

    void OnGUI()
    {
        if (GUILayout.Button("调整位置"))
        {
            GetComponent<Relatee>().UpdatePos();
        }    
    }
}
