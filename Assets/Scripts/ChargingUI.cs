using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUI : MonoBehaviour
{
    [SerializeField]
    GameObject center;
    [SerializeField]
    GameObject chargingArrow;
    [SerializeField]
    GameObject lastRing;
    [SerializeField] float fireSpeed;

    bool fullyCharged;
    float chargingTime;
    bool charging;
    bool pressingJoystick;
    bool isFiring;
    bool backToPos;
    Vector3 joystickVector;
    Vector3 lastPos;
    float tiempoCastigo;
    float arrowDirectX;
    float arrowDirectY;
    bool castigo;
    PlayerMovement playerMove;

    public bool Charging
    {
        get
        {
            return charging;
        }
    }
    public float TiempoCastigo
    {
        get
        {
            return tiempoCastigo;
        }
    }

    private void Start()
    {
        chargingTime = 0;
        castigo = false;
        joystickVector = Vector3.zero;
        tiempoCastigo = 0;
        playerMove = GetComponent<PlayerMovement>();
        charging = false;
        fullyCharged = false;
    }

    public float GetChargingTime()
    {
        return Mathf.Round(chargingTime);
    }
    public void StarCharging ()
    {
        if (!charging && !castigo && !isFiring)
        {
            playerMove.enabled = false;
            charging = true;
            lastPos = transform.position;
        }
    }
    public void StopCharging ()
    {
        if (charging && !fullyCharged)
        {
            backToPos = true;
            charging = false;
            tiempoCastigo = 3f;
            castigo = true;
            chargingArrow.SetActive(false);
        }
    }
    private void Update()
    {
        if (castigo)
        {
            tiempoCastigo -= Time.deltaTime;
            if (tiempoCastigo <= 0)
            {
                castigo = false;
            }
        }

        //Carga Incompleta
        if (backToPos)
        {
            if (transform.position != lastPos)
            {
                transform.LookAt(lastPos);
                transform.Translate(Vector3.forward * Time.deltaTime * fireSpeed);
            }
            if (Vector3.Distance(transform.position, lastPos) < 0.5f)
            {
                transform.position = lastPos;
                playerMove.enabled = true;
                backToPos = false;
            }
        }
        if (charging)
        {
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, 1.5f);
        }
        //Carga Completa
        if (gameObject.transform.position == center.transform.position)
        {
            transform.eulerAngles = Vector3.zero;
            charging = false;
            fullyCharged = true;
            chargingArrow.SetActive(true);

            if (chargingTime < 10)
                chargingTime += Time.deltaTime;
        }
        if (fullyCharged && !isFiring)
        {
            //Apuntar
                if (arrowDirectX == 0 && arrowDirectY == 0)
                {
                    pressingJoystick = false;
                }
                if (arrowDirectX != 0 || arrowDirectY != 0)
                {
                    float angulo = Mathf.Atan2(arrowDirectX, arrowDirectY) * Mathf.Rad2Deg;
                    joystickVector.y = angulo;
                    gameObject.transform.eulerAngles = joystickVector;
                }
        }
    }
    public void Fire ()
    {
        if (fullyCharged)
        {
            isFiring = true;
            chargingArrow.SetActive(false);
            transform.Translate(-Vector3.forward * Time.deltaTime * fireSpeed);
        }
    }
    public void ArrowDirection (float x, float y)
    {
        arrowDirectX = x * -1;
        arrowDirectY = y;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isFiring && collision.gameObject == lastRing)
        {
            fullyCharged = false;
            isFiring = false;
            playerMove.enabled = true;
            playerMove.r = 68;
            chargingTime = 0;
            playerMove.t = (Mathf.Atan2(transform.position.z, transform.position.x) / 2);
            tiempoCastigo = 3f;
            castigo = true;
        }
    }
}
