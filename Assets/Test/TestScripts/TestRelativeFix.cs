using UnityEngine;
using CrazyBox.Tools;
using CrazyBox.Systems;

public class TestRelativeFix : MonoBehaviour {

    void OnGUI()
    {
        if (GUILayout.Button("调整位置"))
        {
            EventsManager.Instance.Dispatch(RelativeFix.evtFixPos);
        }    
    }
}
