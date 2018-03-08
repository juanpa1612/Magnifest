using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float w;
    public float r;
    public float t;

    float rDest;
    bool singlePulsePad;
    bool direccion;
    bool changeRing;
    float timeRingChange;

    [SerializeField] float valorCrecRad;
    
    private bool choque;
    private float tiempoRecuperacion;

    [SerializeField]
    float timeRingMax;

    int vidas;

    void Start ()
    {
        vidas = 3;
        timeRingChange = timeRingMax;
        t = 0;
        r = valorCrecRad;
        w = 2;
        singlePulsePad = false;
        direccion = false;
        changeRing = false;
        rDest = 0;
        
	}

    public int GetVidas()
    {
        return vidas;
    }

    public void RestarVidas()
    {
        vidas--;
    }

    public void Reset()
    {
        r = 17;
        t = 0;
        changeRing = false;
        rDest = 0;
    }

    void Update ()
    {
        transform.position = new Vector3(Mathf.Cos(w * t), 0, Mathf.Sin(w * t)) * r;
        //Collision
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
        
        if (changeRing)
        {
            timeRingChange -= Time.deltaTime;
            r = Mathf.Lerp(r, rDest, timeRingChange);
            if (timeRingChange<=0)
            {
                changeRing = false;
                timeRingChange=timeRingMax;
                r=Mathf.Round(r);
            }
        }
        
       
    }
    public void ChangeRing (bool addOrSub)
    {
        if (addOrSub && r < 65 && !changeRing)
        {
            rDest = r + valorCrecRad;
            changeRing = true;
        }
        if (!addOrSub && r > 18 && !changeRing)
        {
            rDest = r - valorCrecRad;
            changeRing = true;
        }
    }
    public void ChangeDirection (bool right)
    {
        if (right)
            direccion = true;
        else
            direccion = false;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !choque && (!collision.GetComponent<PlayerMovement>().enabled))
        {
            if (collision.GetComponent<ChargingUI>().Charging == false && !collision.GetComponent<DeathScript>().enabled && !GetComponent<ChargingUI>().Charging)
            {
                Debug.Log("Me Chocaron");
                choque = true;
                rDest += collision.gameObject.GetComponent<PlayerMovement>().r;
                changeRing = true;
                transform.rotation = collision.gameObject.transform.rotation;
                if (rDest > 68)
                {
                    GetComponent<DeathScript>().enabled = true;
                    GetComponent<ChargingUI>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}
