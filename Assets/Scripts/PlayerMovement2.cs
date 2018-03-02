using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
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
    [SerializeField]
    float timeRingMax;
    private bool choque;
    private float tiempoRecuperacion;

    void Start ()
    {
        timeRingChange = timeRingMax;
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
        if (choque)
        {
            tiempoRecuperacion -= Time.deltaTime;
            if (tiempoRecuperacion <= 0)
            {
                choque = false;
                tiempoRecuperacion = 2f;
            }
        }
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
                timeRingChange= timeRingMax;
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
                timeRingChange = timeRingMax;
                r = Mathf.Round(r);
            }
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") > 0.8f)
        {
            direccion = true;
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") < -0.8f)
        {
            direccion = false;
        }
        if (Input.GetButtonDown("Right Bumper 2") && r < 65 && (!changeRingPos && !changeRingNeg))
        {
            rDest = r + valorCrecRad;
            changeRingPos = true;
        }
        if (Input.GetButtonDown("Left Bumper 2") && r > 18  && (!changeRingPos && !changeRingNeg))
        {
            rDest = r - valorCrecRad;
            changeRingNeg = true;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !choque && (!collision.GetComponent<PlayerMovement>().enabled))
        {
            if (collision.GetComponent<ChargingUI>().Charging == false && !collision.GetComponent<DeathScript>().enabled && !GetComponent<ChargingUI>().Charging)
            {
                choque = true;
                rDest += collision.gameObject.GetComponent<PlayerMovement>().r;
                changeRingPos = true;
                transform.rotation = collision.gameObject.transform.rotation;
                if (rDest > 68)
                {
                    GetComponent<DeathScript>().enabled = true;
                    GetComponent<ChargingUI2>().enabled = false;
                    this.enabled = false;
                }
            }
        }

        
    }
}
