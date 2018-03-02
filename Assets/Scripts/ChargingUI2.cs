using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUI2 : MonoBehaviour
{
    [SerializeField]
    GameObject center;
    [SerializeField]
    GameObject chargingArrow;
    [SerializeField]
    GameObject lastRing;
    [SerializeField] float fireSpeed;
    public bool fullyCharged;
    float chargingTime;
    bool charging;
    bool pressingJoystick;
    bool isFiring;
    bool backToPos;
    Vector3 joystickVector;
    Vector3 lastPos;
    float tiempoCastigo;
    bool castigo;
    PlayerMovement2 playerMove;
    Shoot playerShoot;

    public bool Charging
    {
        get
        {
            return charging;
        }
    }

    private void Start()
    {
        chargingTime = 0;
        castigo = false;
        joystickVector = Vector3.zero;
        tiempoCastigo = 0;
        playerMove = GetComponent<PlayerMovement2>();
    }

    public float GetChargingTime()
    {
        return Mathf.Round(chargingTime);
    }

    private void Update()
    {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]+i);
        }

        Debug.Log(Input.GetAxis("RightTrigger2"));
        if (castigo)
        {
            tiempoCastigo -= Time.deltaTime;
            if (tiempoCastigo <= 0)
            {
                castigo = false;
            }
        }
        if (Input.GetAxis("RightTrigger2") > 0 && !charging &&!castigo && !isFiring)
        {
            playerMove.enabled = false;
            charging = true;
            lastPos = transform.position;
        }
        //Dejo de Cargar
        else if (Input.GetAxis("RightTrigger2") < 0.1f && charging && !fullyCharged)
        {
            backToPos = true;
            charging = false;
            tiempoCastigo = 3f;
            castigo = true;
            chargingArrow.SetActive(false);
        }
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
            float x = Input.GetAxis("LeftJoystick2Horizontal") * -1;
            float y = Input.GetAxis("LeftJoystick2Vertical");
                if (x == 0 && y == 0)
                {
                    pressingJoystick = false;
                }
                if (x != 0 || y != 0)
                {
                    float angulo = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
                    joystickVector.y = angulo;
                    gameObject.transform.eulerAngles = joystickVector;
                }
        }
        //Lanzar
        if (fullyCharged && Input.GetAxis("RightTrigger2") < 0.1f)
        {
            isFiring = true;
            chargingArrow.SetActive(false);
            transform.Translate(-Vector3.forward * Time.deltaTime * fireSpeed);
        }
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
            //playerMove.t = (Mathf.Acos(transform.position.x / 68) * Mathf.Rad2Deg) / (2 * Mathf.Rad2Deg);
            playerMove.t = (Mathf.Atan2(transform.position.z, transform.position.x) / 2);
            tiempoCastigo = 3f;
            castigo = true;
        }
    }
}
