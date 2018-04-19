using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {
    bool busy;
    int countCol;
    
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
        countCol = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (countCol == 0)
        {
            busy = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countCol++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countCol--;
        }
    }
}
