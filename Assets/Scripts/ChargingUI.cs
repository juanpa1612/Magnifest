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
        Debug.Log(Input.GetAxis("RightTrigger"));
        if (Input.GetAxis("RightTrigger") > 0 && !charging)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            charging = true;
        }

        else if (Input.GetAxis("RightTrigger") < 0.1f && charging)
        {
            charging = false;
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerMovement>().r = 65;
            chargingArrow.SetActive(false);
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
            Debug.Log(chargingTime);
        }
        Debug.Log(Input.GetAxis("RightJoystickVertical"));
    }
}
