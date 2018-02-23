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
	// Use this for initialization
	void Start () {
        //r = 68;
        estado = 1;
        choque = false;
        tiempoRecuperacion = 2f;
	}
	
	// Update is called once per frame
	void Update () {
        if (estado == 1)
        {
            transform.position = new Vector3(Mathf.Cos(w * t), 0, Mathf.Sin(w * t)) * r;
            t += Time.deltaTime;
        }
        else if(estado==2)
        {
            transform.Translate(-Vector3.forward*10);
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
        if (collision.CompareTag("Player")&&!choque&&(!collision.GetComponent<PlayerMovement>().enabled))
        {
            choque = true;
            float division = Mathf.Round(collision.gameObject.GetComponent<ChargingUI>().GetChargingTime()) / 2;
            if (division < 1)
            {
                division = 1;
            }
            r +=(division*17);
            transform.rotation = collision.gameObject.transform.rotation;
            if (r > 68)
            {
                estado = 2;
            }
        }
    }
}
