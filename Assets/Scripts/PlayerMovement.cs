using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float w;
    [SerializeField]
    public float r;
    float t;
    float rDest;
    bool singlePulsePad;
    bool direccion;
    bool changeRingPos;
    bool changeRingNeg;
    float timeRingChange;

	void Start ()
    {
        timeRingChange = 0.3f;
        t = 0;
        r = 19;
        w = 2f;
        singlePulsePad = false;
        direccion = true;
        changeRingNeg = false;
        changeRingPos = false;
        rDest = 0;
        
	}
	

	void Update ()
    {
        transform.position = new Vector3(r * Mathf.Cos(w * t), 0, r * Mathf.Sin(w * t));

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
        if (Input.GetAxis("LeftJoystickVertical") == 1)
        {
            direccion = true;
        }
        if (Input.GetAxis("LeftJoystickVertical") == -1)
        {
            direccion = false;
        }
        if (Input.GetAxis("RightJoystickVertical") == 1 && r <= 18 && !singlePulsePad && (!changeRingPos && !changeRingNeg))
        {
            rDest = r + 19;
            changeRingPos = true;
            singlePulsePad = true;
        }
        if (Input.GetAxis("RightJoystickVertical") == -1 && r >= 90 && !singlePulsePad && (!changeRingPos && !changeRingNeg))
        {
            rDest = r - 19;
            changeRingNeg = true;
            singlePulsePad = true;
        }
    }
}
