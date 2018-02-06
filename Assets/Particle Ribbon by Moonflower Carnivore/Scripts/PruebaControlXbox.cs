using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaControlXbox : MonoBehaviour {
    float r;
    float t;
    float w;
    bool singlePulsePad;
    bool direccion;
	// Use this for initialization
	void Start () {
        t = 0;
        r = 2;
        w = 2f;
        singlePulsePad = false;
        direccion = true;
        
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
        if (Input.GetAxis("AnalogoIXbox") == 1)
        {
            direccion = true;
        }
        if (Input.GetAxis("AnalogoIXbox") == -1)
        {
            direccion = false;
        }
        if (Input.GetAxis("AnalogoDXbox") == 1 && r <= 4 && !singlePulsePad)
        {
            r += 2;
            //r=Mathf.SmoothStep(r,r+2,0.5f);
            singlePulsePad = true;
        }
        if (Input.GetAxis("AnalogoDXbox") == -1 && r >= 4 && !singlePulsePad)
        {
            r -= 2;
            singlePulsePad = true;
        }
        if (Input.GetAxis("AnalogoDXbox")==0)
        {
            singlePulsePad = false;
        }
        
    }
}
