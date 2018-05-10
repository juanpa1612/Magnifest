using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]float t;
    [SerializeField]float r;
    [SerializeField]float w; 
    int estado;
    bool choque;
    float tiempoRecuperacion;
    private bool changeRingPos;
    private float timeRingChange;
    float rDest;

    // Use this for initialization
    void Start () {
        //r = 68;
        estado = 1;
        choque = false;
        tiempoRecuperacion = 2f;
        timeRingChange = 0.5f;
        changeRingPos = false;
	}
	
	void Update () {
        if (estado == 1)
        {
            transform.position = new Vector3(Mathf.Cos(w * t), 0, Mathf.Sin(w * t)) * r;
            t += Time.deltaTime;
            if (changeRingPos)
            {
                timeRingChange -= Time.deltaTime;
                r = Mathf.Lerp(r, rDest, timeRingChange);
                if (timeRingChange <= 0)
                {
                    changeRingPos = false;
                    rDest = 0;
                    timeRingChange = 0.5f;
                    r = Mathf.Round(r);
                }
            }
        }
        else if(estado==2)
        {
            transform.Translate(-Vector3.forward*5);
        }
        else if (estado == 3)
        {
            transform.position = new Vector3(1000,1000,1000);
        }
        if (choque)
        {
            tiempoRecuperacion -= Time.deltaTime;
            if (tiempoRecuperacion <= 0)
            {
                choque = false;
                tiempoRecuperacion = 2f;
            }
        }
    }

    void OnBecomeInvisible()
    {
        estado = 3;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player")&&!choque&&(!collision.GetComponent<PlayerMovement>().enabled) && collision.GetComponent<Charging>().IsCharging == false)
        {
            choque = true;
            rDest += collision.gameObject.GetComponent<PlayerMovement>().radius + r;
            changeRingPos = true;
            transform.rotation = collision.gameObject.transform.rotation;
            if (rDest > 68)
            {
                estado = 2;
            }
        }
    }

}
