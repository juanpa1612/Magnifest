using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float w;
    public float r;
    public float t;

    float rDest;
    bool singlePulsePad;
    bool direccion;
    bool changeRingPos;
    bool changeRingNeg;
    float timeRingChange;

    [SerializeField] float valorCrecRad;

	void Start ()
    {
        timeRingChange = 0.5f;
        t = 0;
        r = valorCrecRad;
        w = 2;
        singlePulsePad = false;
        direccion = false;
        changeRingNeg = false;
        changeRingPos = false;
        rDest = 0;
        
	}
	

	void Update ()
    {
        
        transform.position = new Vector3(Mathf.Cos(w * t), 0, Mathf.Sin(w * t)) * r;
        Debug.Log("X = " + transform.position.x + " Z = " + transform.position.z + " t = " + t);
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
                timeRingChange=0.5f;
                singlePulsePad = false;
                //r += 1;
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
                timeRingChange = 0.5f;
                singlePulsePad = false;
                //r -= 1;
                r = Mathf.Round(r);
            }
        }
        if (Input.GetAxis("LeftJoystickHorizontal") > 0.8f)
        {
            direccion = true;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") < -0.8f)
        {
            direccion = false;
        }
        Debug.Log(Input.GetButton("Fire1"));
        if (Input.GetButtonDown("Right Bumper") && r < 65 /*&& !singlePulsePad*/ && (!changeRingPos && !changeRingNeg))
        {
            rDest = r + valorCrecRad;
            changeRingPos = true;
            //singlePulsePad = true;
        }
        if (Input.GetButtonDown("Left Bumper") && r > 18 /*&& !singlePulsePad*/ && (!changeRingPos && !changeRingNeg))
        {
            rDest = r - valorCrecRad;
            changeRingNeg = true;
            //singlePulsePad = true;
        }
    }
}
