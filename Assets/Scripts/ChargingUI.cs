using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUI : MonoBehaviour
{
    [SerializeField]
    GameObject player;
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
    Vector3 joystickVector;
    float tiempoCastigo;
    bool castigo;
    float tiempoDisparo;
    bool once;
    PlayerMovement playerMove;
    Shoot playerShoot;

    private void Start()
    {
        once = false;
        castigo = false;
        joystickVector = Vector3.zero;
        tiempoCastigo = 0;
        tiempoDisparo = 2f;
        playerMove = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<Shoot>();
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
        if (Input.GetAxis("RightTrigger") > 0 && !charging &&!castigo && !isFiring)
        {
            playerMove.enabled = false;
            charging = true;
        }

        else if (Input.GetAxis("RightTrigger") < 0.1f && charging && !fullyCharged)
        {
            charging = false;
            //GetComponent<PlayerMovement>().enabled = true;
            //GetComponent<PlayerMovement>().r = 65;
            tiempoCastigo = 3f;
            castigo = true;
            chargingArrow.SetActive(false);
            playerShoot.enabled = true;
            playerShoot.PasarEstadoDisparo();
            //fullyCharged = false;
        }

        if (charging)
        {
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, 0.5f);
        }
        //Carga Completa
        if (player.transform.position == center.transform.position)
        {
            transform.eulerAngles = Vector3.zero;
            charging = false;
            fullyCharged = true;
            chargingArrow.SetActive(true);

            if (chargingTime < 5)
                chargingTime += Time.deltaTime;
        }
        if (fullyCharged && !isFiring)
        {
            //Apuntar
            float x = Input.GetAxis("LeftJoystickHorizontal") * -1;
            float y = Input.GetAxis("LeftJoystickVertical");
                if (x == 0 && y == 0)
                {
                    pressingJoystick = false;
                }
                if (x != 0 || y != 0)
                {
                    float angulo = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
                    joystickVector.y = angulo;
                    player.transform.eulerAngles = joystickVector;
                }
        }
        //Lanzar
        if (fullyCharged && Input.GetAxis("RightTrigger") < 0.1f)
        {
            isFiring = true;
            chargingArrow.SetActive(false);
            transform.Translate(-Vector3.forward * Time.deltaTime * fireSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isFiring && collision.gameObject == lastRing)
        {
            fullyCharged = false;
            isFiring = false;
            Debug.Log("Impactó en " + transform.position.x);
            playerMove.enabled = true;
            playerMove.r = 68;

            //playerMove.t = (Mathf.Acos(transform.position.x / 68) * Mathf.Rad2Deg) / (2 * Mathf.Rad2Deg);
            playerMove.t = (Mathf.Atan2(transform.position.z, transform.position.x) / 2);
            tiempoCastigo = 3f;
            castigo = true;
        }
    }
}
