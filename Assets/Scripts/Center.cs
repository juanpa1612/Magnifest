using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {
    bool busy;
    
    public bool GetBusy()
    {
        return busy;
    }

    public void SetBusy(bool state)
    {
        busy = state;
    }
	// Use this for initialization
	void Start () {
        busy = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
