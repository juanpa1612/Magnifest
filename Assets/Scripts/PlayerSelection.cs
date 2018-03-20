using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject[] skins;
    [SerializeField]
    GameObject arrow1, arrow2;
    [SerializeField]
    ScriptablePlayer player1, player2, player3, player4;
    bool[] skinPreview;
    bool singlePulse1, singlePulse2;
    public bool player1Ready, player2Ready;
    public int arrowPos1, arrowPos2;
    Vector3 offset;

	void Start ()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            DontDestroyOnLoad(skins[i]);
        }
        offset = new Vector3(0, -6, 0);
        arrowPos1 = 0;
        arrowPos2 = 1;
        arrow1.transform.position = skins[arrowPos1].transform.position + offset;
        arrow2.transform.position = skins[arrowPos2].transform.position + offset;
    }
	
	void Update ()
    {
        bool add;
        bool add2;
        Debug.Log("A 1" + Input.GetButtonDown("AButton1"));
        Debug.Log("A 2" + Input.GetButtonDown("AButton2"));
        Debug.Log("B 1" + Input.GetButtonDown("BButton1"));
        Debug.Log("B 2" + Input.GetButtonDown("BButton1"));

        #region Joystick_1
        if (arrowPos1 >=0 && arrowPos1 <3)
        {
            if (Input.GetAxis("LeftJoystickHorizontal") >= 0.8 && !singlePulse1 && !player1Ready)
            {
                arrowPos1++;
                singlePulse1 = true;
                add = true;
                ChangeSelection(add);
            }
        }
        if (arrowPos1 >0 && arrowPos1 <=3)
        {
            if (Input.GetAxis("LeftJoystickHorizontal") <= -0.8 && !singlePulse1 && !player1Ready)
            {
                arrowPos1--;
                singlePulse1 = true;
                add = false;
                ChangeSelection(add);
            }
        }
        if (Input.GetAxis("LeftJoystickHorizontal") <= 0.7f && Input.GetAxis("LeftJoystickHorizontal") >= -0.7f)
            singlePulse1 = false;
        #endregion
        #region Joystick_2
        if (arrowPos2 >= 0 && arrowPos2 < 3)
        {
            if (Input.GetAxis("LeftJoystick2Horizontal") >= 0.8 && !singlePulse2 && !player2Ready)
            {
                arrowPos2++;
                singlePulse2 = true;
                add2 = true;
                ChangeSelection2(add2);
            }
        }
        if (arrowPos2 > 0 && arrowPos2 <= 3)
        {
            if (Input.GetAxis("LeftJoystick2Horizontal") <= -0.8 && !singlePulse2 && !player2Ready)
            {
                arrowPos2--;
                singlePulse2 = true;
                add2 = false;
                ChangeSelection2(add2);
            }
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") <= 0.7f && Input.GetAxis("LeftJoystick2Horizontal") >= -0.7f)
            singlePulse2 = false;

        #endregion

        if (Input.GetButtonDown("AButton1"))
            Player1Ready();
        if (Input.GetButtonDown("AButton2"))
            Player2Ready();

        if (Input.GetButtonDown("BButton1"))
            player1Ready = false;
        if (Input.GetButtonDown("BButton2"))
            player2Ready = false;

        if (player1Ready && player2Ready)
        {
            SceneManager.LoadScene("TestLevel");
        }
    }

    void Player1Ready ()
    {
        
        player1Ready = true;
        switch (arrowPos1)
        {
            default:
                break;
            case 0:
                player1.actualSkin = player1.skins[0];
                break;
            case 1:
                player1.actualSkin = player1.skins[1];
                break;
            case 2:
                player1.actualSkin = player1.skins[0];
                break;
            case 3:
                player1.actualSkin = player1.skins[1];
                break;
        }
       
    }
    void Player2Ready ()
    {
        player2Ready = true;
        switch (arrowPos2)
        {
            default:
                break;
            case 0:
                player2.actualSkin = player2.skins[0];
                break;
            case 1:
                player2.actualSkin = player2.skins[1];
                break;
            case 2:
                player2.actualSkin = player2.skins[0];
                break;
            case 3:
                player2.actualSkin = player2.skins[1];
                break;
        }
    }
    public void ChangeSelection (bool add)
    {
        //Está disponible?
        if (arrowPos2 != arrowPos1)
            arrow1.transform.position = skins[arrowPos1].transform.position + offset;
        //Si Está ocupado
        else if (arrowPos2 == arrowPos1 && add)
        {
            if (arrowPos2 == 3)
            {
                arrowPos1 = 0;
                arrow1.transform.position = skins[arrowPos1].transform.position + offset;
            }
            else
            {
                arrowPos1++;
                arrow1.transform.position = skins[arrowPos1].transform.position + offset;
            }

        }
        else if (arrowPos2 == arrowPos1 && !add)
        {
            if (arrowPos2 == 0)
            {
                arrowPos1 = 3;
                arrow1.transform.position = skins[arrowPos1].transform.position + offset;
            }
            else
            {
                arrowPos1--;
                arrow1.transform.position = skins[arrowPos1].transform.position + offset;
            }
        }
    }
    public void ChangeSelection2 (bool add)
    {
        //Está disponible?
        if (arrowPos1 != arrowPos2)
            arrow2.transform.position = skins[arrowPos2].transform.position + offset;
        //Si Está ocupado
        else if (arrowPos2 == arrowPos1 && add)
        {
            if (arrowPos1 == 3)
            {
                arrowPos2 = 0;
                arrow2.transform.position = skins[arrowPos2].transform.position + offset;
            }
            else
            {
                arrowPos2++;
                arrow2.transform.position = skins[arrowPos2].transform.position + offset;
            }

        }
        else if (arrowPos2 == arrowPos1 && !add)
        {
            if (arrowPos1 == 0)
            {
                arrowPos2 = 3;
                arrow2.transform.position = skins[arrowPos2].transform.position + offset;
            }
            else
            {
                arrowPos2--;
                arrow2.transform.position = skins[arrowPos2].transform.position + offset;
            }
        }
    }
}
