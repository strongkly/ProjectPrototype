using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyBox.Systems;

public class TestEventSystem : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        evtIdx = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    int evtIdx;

    void OnGUI()
    {
        GUILayout.Label(string.Format("evt idx now：{0}", evtIdx));
        if (GUILayout.Button("新增处理函数"))
        {
            EventsManager.Instance.AddListener<int>(string.Format("evtIdx_{0}", evtIdx), EvtInt);
        }
        if (GUILayout.Button("移除处理函数"))
        {
            EventsManager.Instance.RemoveListener<int>(string.Format("evtIdx_{0}", evtIdx), EvtInt);
        }
        if (GUILayout.Button("触发事件"))
        {
            EventsManager.Instance.Dispatch(string.Format("evtIdx_{0}", evtIdx), evtIdx);
        }
    }

    void EvtInt(int i = 0)
    {
        Debug.LogError("@@@");
    }
}
