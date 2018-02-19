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

    public bool fullyCharged;
    float chargingTime;
    bool charging;
    bool pressingJoystick;
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
        joystickVector = new Vector3(-90,0,0);
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
        if (fullyCharged)
        {
            
            float x = Input.GetAxis("LeftJoystickHorizontal")*-1;
            float y = Input.GetAxis("LeftJoystickVertical");
            if (x == 0 && y ==0)
            {
                pressingJoystick = false;
            }
            if (x != 0 || y != 0)
            {
                float angulo = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
                joystickVector.z = angulo;
                //Debug.Log(angulo);
                player.transform.eulerAngles = joystickVector;
            }
        }
        //Debug.Log(Input.GetAxis("RightTrigger"));
        if (Input.GetAxis("RightTrigger") > 0 && !charging &&!castigo)
        {
            playerMove.enabled = false;
            charging = true;
        }

        else if (Input.GetAxis("RightTrigger") < 0.1f && charging)
        {
            charging = false;
            //GetComponent<PlayerMovement>().enabled = true;
            //GetComponent<PlayerMovement>().r = 65;
            tiempoCastigo = 4f;
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

        if (player.transform.position == center.transform.position)
        {
            fullyCharged = true;
            chargingArrow.SetActive(true);

            if (chargingTime < 5)
                chargingTime += Time.deltaTime;
            //Debug.Log(chargingTime);
        }
        //Debug.Log(Input.GetAxis("RightJoystickVertical"));
    }
}
