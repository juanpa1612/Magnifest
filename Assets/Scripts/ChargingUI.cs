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

    public bool fullyCharged;
    float chargingTime;
    bool charging;
    bool pressingJoystick;
    Vector3 joystickVector;

    private void Start()
    {
        joystickVector = new Vector3(-90,0,0);
    }
    private void Update()
    {
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
                Debug.Log(angulo);
                player.transform.eulerAngles = joystickVector;
            }
        }

        if (Input.GetButtonDown("AButton"))
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            charging = true;
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
            Debug.Log(chargingTime);
        }
        Debug.Log(Input.GetAxis("RightJoystickVertical"));
    }
}
