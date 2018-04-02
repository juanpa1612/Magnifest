using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectionNew : MonoBehaviour
{
    [SerializeField]
    MainMenu mainMenu;
    [SerializeField]
    ScriptablePlayer[] scriptablePlayers;
    [SerializeField]
    Skins skinsReference;
    [SerializeField]
    GameObject[] skinsPlayers;
    [SerializeField]
    GameObject[] availableSkins;
    [SerializeField]
    Text[] txtsToJoin;
    [SerializeField]
    bool[] playerJoined;

    int[] actualSkin;

    bool singlePulse1, singlePulse2, singlePulse3, singlePulse4;
    Vector3[] initialPos;

	void Start ()
    {
        actualSkin = new int[4];
        actualSkin[0] = 0;
        actualSkin[1] = 0;
        actualSkin[2] = 0;
        actualSkin[3] = 0;

        initialPos = new Vector3[4];
        for (int i = 0; i < initialPos.Length -1; i++)
        {
            initialPos[i] = skinsPlayers[i].transform.position;
        }
	}

	void Update ()
    {
        if (mainMenu.skinSelection.alpha == 1)
        {
            if (Input.GetButtonDown("AButton1") && !playerJoined[0])
            {
                PlayerJoin(0);
                playerJoined[0] = true;
            }
            if (Input.GetButtonDown("AButton2") && !playerJoined[1])
            {
                PlayerJoin(1);
                playerJoined[1] = true;
            }
            if (Input.GetButtonDown("AButton3") && !playerJoined[2])
            {
                PlayerJoin(2);
                playerJoined[2] = true;
            }
            if (Input.GetButtonDown("AButton4") && !playerJoined[3])
            {
                PlayerJoin(3);
                playerJoined[3] = true;
            }
        }
        #region Inputs Joystick 1
        if (Input.GetAxis("LeftJoystickHorizontal") >= 0.7f &&!singlePulse1)
        {
            Joystick1(true);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") <= -0.7f && !singlePulse1)
        {
            Joystick1(false);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") == 0)
            singlePulse1 = false;
        #endregion
        #region Inputs Joystick 2
        if (Input.GetAxis("LeftJoystick2Horizontal") >= 0.7f && !singlePulse1)
        {
            Joystick2(true);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") <= -0.7f && !singlePulse1)
        {
            Joystick2(false);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") == 0)
            singlePulse2 = false;
        #endregion
        #region Players Are Ready
        if (Input.GetButtonDown("AButton1") && playerJoined[0])
            PlayersReady(0);
        if (Input.GetButtonDown("AButton1") && playerJoined[0])
            PlayersReady(1);
        if (Input.GetButtonDown("AButton1") && playerJoined[0])
            PlayersReady(2);
        if (Input.GetButtonDown("AButton1") && playerJoined[0])
            PlayersReady(3);
        #endregion
        if (Input.GetButtonDown("StartButton"))
            SceneManager.LoadScene("TestLevel");
    }
    public void PlayersReady (int playerNumber)
    {
        scriptablePlayers[playerNumber].actualSkin = skinsReference.skins[actualSkin[playerNumber]];
    }
    public void PlayerJoin (int playerNumber)
    {
        switch (playerNumber)
        {
            case 0:
                skinsPlayers[0].SetActive(true);
                //GameObject.Instantiate(availableSkins[0], Camera.main.ScreenToWorldPoint(txtsToJoin[0].transform.position),Quaternion.identity);
                txtsToJoin[0].CrossFadeAlpha(0, 1, true);
                break;
            case 1:
                skinsPlayers[1].SetActive(true);
                //GameObject.Instantiate(availableSkins[0], Camera.main.ScreenToWorldPoint(txtsToJoin[1].transform.position), Quaternion.identity);
                txtsToJoin[1].CrossFadeAlpha(0, 1, true);
                break;
            case 2:
                GameObject.Instantiate(availableSkins[0], skinsPlayers[2].transform);
                txtsToJoin[2].CrossFadeAlpha(0, 1, true);
                break;
            case 3:
                GameObject.Instantiate(availableSkins[0], skinsPlayers[3].transform);
                txtsToJoin[3].CrossFadeAlpha(0, 1, true);
                break;
        }
    }
    public void Joystick1 (bool right)
    {
        if (playerJoined[0])
        {
            if (right && actualSkin[0] < skinsPlayers.Length - 1)
                actualSkin[0]++;
            if (!right && actualSkin[0] >= 1)
                actualSkin[0]--;

            GameObject lastSkin = skinsPlayers[0];
            lastSkin.SetActive(false);
            skinsPlayers[0] = Instantiate(skinsReference.skins[actualSkin[0]], initialPos[0], Quaternion.identity);
            skinsPlayers[0].SetActive(true);
        }
    }
    public void Joystick2(bool right)
    {
        if (playerJoined[1])
        {
            if (right && actualSkin[1] < skinsPlayers.Length - 1)
                actualSkin[1]++;
            if (!right && actualSkin[1] >= 1)
                actualSkin[1]--;

            GameObject lastSkin = skinsPlayers[1];
            lastSkin.SetActive(false);
            skinsPlayers[1] = Instantiate(availableSkins[actualSkin[1]], initialPos[1], Quaternion.identity);
            skinsPlayers[1].SetActive(true);
        }
    }
}
