using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaControlXbox : MonoBehaviour {
    float r;
    float t;
    float w;
    float rDest;
    bool singlePulsePad;
    bool direccion;
    bool changeRingPos;
    bool changeRingNeg;
    float timeRingChange;
	// Use this for initialization
	void Start () {
        timeRingChange = 0.3f;
        t = 0;
        r = 2;
        w = 2f;
        singlePulsePad = false;
        direccion = true;
        changeRingNeg = false;
        changeRingPos = false;
        rDest = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(r * Mathf.Cos(w * t), r * Mathf.Sin(w * t), 0);
        if (!direccion)
        {
            t += Time.deltaTime;
        }
        else
        {
            t -= Time.deltaTime;
        }
        
        if (changeRingPos&&!changeRingNeg)
        {
            timeRingChange -= Time.deltaTime;
            r = Mathf.Lerp(r, rDest, timeRingChange);
            if (timeRingChange<=0)
            {
                changeRingPos = false;
                timeRingChange=0.3f;
                singlePulsePad = false;
                r=Mathf.Round(r);
            }
        }
        if (!changeRingPos && changeRingNeg)
        {
            timeRingChange -= Time.deltaTime;
            r = Mathf.Lerp(r, rDest, timeRingChange);
            if (timeRingChange <= 0)
            {
                changeRingNeg = false;
                timeRingChange = 0.3f;
                singlePulsePad = false;
                r = Mathf.Round(r);
            }
        }
        if (Input.GetAxis("AnalogoIXbox") == 1)
        {
            direccion = true;
        }
        if (Input.GetAxis("AnalogoIXbox") == -1)
        {
            direccion = false;
        }
        if (Input.GetAxis("AnalogoDXbox") == 1 && r <= 4 && !singlePulsePad &&(!changeRingPos&&!changeRingNeg))
        {
            rDest = r + 2;
            changeRingPos = true;
            singlePulsePad = true;
        }
        if (Input.GetAxis("AnalogoDXbox") == -1 && r >= 4 && !singlePulsePad && (!changeRingPos && !changeRingNeg))
        {
            rDest = r - 2;
            changeRingNeg = true;
            singlePulsePad = true;
        }
    }
}
