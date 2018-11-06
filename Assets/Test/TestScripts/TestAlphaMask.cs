using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlphaMask : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    	
	}


    void OnGUI()
    {
        if (GUILayout.Button("update mask"))
        {
            gameObject.GetComponent<CrazyBox.Tools.Mask>().UpdateMask();
        }
    }
}
