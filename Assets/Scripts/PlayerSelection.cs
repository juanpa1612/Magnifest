using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject[] players;
    [SerializeField]
    GameObject arrow1, arrow2;

    bool[] skinPreview;
    bool singlePulse1, singlePulse2;
    public int arrowPos1, arrowPos2;
    Vector3 offset;

	void Start ()
    {
        for (int i = 0; i < players.Length; i++)
        {
            DontDestroyOnLoad(players[i]);
        }
        offset = new Vector3(0, 10, 0);
        arrowPos1 = 0;
        arrowPos2 = 1;
        arrow1.transform.position = players[arrowPos1].transform.position + offset;
        arrow2.transform.position = players[arrowPos2].transform.position + offset;
    }
	

	void Update ()
    {
        bool add;
        bool add2;
        #region Joystick_1
        if (arrowPos1 >=0 && arrowPos1 <3)
        {
            if (Input.GetAxis("LeftJoystickHorizontal") >= 0.8 && !singlePulse1)
            {
                arrowPos1++;
                singlePulse1 = true;
                add = true;
                ChangeSelection(add);
            }
        }
        if (arrowPos1 >0 && arrowPos1 <=3)
        {
            if (Input.GetAxis("LeftJoystickHorizontal") <= -0.8 && !singlePulse1)
            {
                arrowPos1--;
                singlePulse1 = true;
                add = false;
                ChangeSelection(add);
            }
        }
        if (Input.GetAxis("LeftJoystickHorizontal") == 0)
            singlePulse1 = false;
        #endregion
        #region Joystick_2
        if (arrowPos2 >= 0 && arrowPos2 < 3)
        {
            if (Input.GetAxis("LeftJoystick2Horizontal") >= 0.8 && !singlePulse2)
            {
                arrowPos2++;
                singlePulse2 = true;
                add2 = true;
                ChangeSelection2(add2);
            }
        }
        if (arrowPos2 > 0 && arrowPos2 <= 3)
        {
            if (Input.GetAxis("LeftJoystick2Horizontal") <= -0.8 && !singlePulse2)
            {
                arrowPos2--;
                singlePulse2 = true;
                add2 = false;
                ChangeSelection2(add2);
            }
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") == 0)
            singlePulse2 = false;
        #endregion

    }
    public void ChangeSelection (bool add)
    {
        //Está disponible?
        if (arrowPos2 != arrowPos1)
            arrow1.transform.position = players[arrowPos1].transform.position + offset;
        //Si Está ocupado
        else if (arrowPos2 == arrowPos1 && add && arrowPos1 <2)
        {
            arrowPos1++;
            arrow1.transform.position = players[arrowPos1].transform.position + offset;
        }
        else if (arrowPos2 == arrowPos1 && !add && arrowPos1 >0)
        {
            arrowPos1--;
            arrow1.transform.position = players[arrowPos1].transform.position + offset;
        }
    }
    public void ChangeSelection2 (bool add)
    {
        //Está disponible?
        if (arrowPos1 != arrowPos2)
            arrow2.transform.position = players[arrowPos2].transform.position + offset;
        //Si Está ocupado
        else if (arrowPos1 == arrowPos2 && add && arrowPos2 < 2)
        {
            arrowPos2++;
            arrow2.transform.position = players[arrowPos2].transform.position + offset;
        }
        else if (arrowPos1 == arrowPos2 && !add && arrowPos2 > 0)
        {
            arrowPos2--;
            arrow2.transform.position = players[arrowPos2].transform.position + offset;
        }
    }
}
