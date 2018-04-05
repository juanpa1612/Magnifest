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
    Text[] txtsToJoin;
    [SerializeField]
    bool[] playerJoined;

    int[] actualSkin;

    bool singlePulse1, singlePulse2, singlePulse3, singlePulse4;
    bool[] playerReady;
    Vector3[] initialPos;

	void Start ()
    {
        actualSkin = new int[4];
        actualSkin[0] = 0;
        actualSkin[1] = 0;
        actualSkin[2] = 0;
        actualSkin[3] = 0;

        playerReady = new bool[4];

        initialPos = new Vector3[4];
        for (int i = 0; i < initialPos.Length; i++)
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
            }
            if (Input.GetButtonDown("AButton2") && !playerJoined[1])
            {
                PlayerJoin(1);
            }
            if (Input.GetButtonDown("AButton3") && !playerJoined[2])
            {
                PlayerJoin(2);
            }
            if (Input.GetButtonDown("AButton4") && !playerJoined[3])
            {
                PlayerJoin(3);
            }
        }
        #region Inputs Joystick 1
        if (Input.GetAxis("LeftJoystickHorizontal") >= 0.7f &&!singlePulse1)
        {
            SkinMovement(true, 0);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") <= -0.7f && !singlePulse1)
        {
            SkinMovement(false, 0);
            singlePulse1 = true;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") == 0)
            singlePulse1 = false;
        #endregion
        #region Inputs Joystick 2
        if (Input.GetAxis("LeftJoystick2Horizontal") >= 0.7f && !singlePulse2)
        {
            SkinMovement(true, 1);
            singlePulse2 = true;
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") <= -0.7f && !singlePulse2)
        {
            SkinMovement(false, 1);
            singlePulse2 = true;
        }
        if (Input.GetAxis("LeftJoystick2Horizontal") == 0)
            singlePulse2 = false;
        #endregion
        #region Inputs Joystick 3
        if (Input.GetAxis("LeftJoystick3Horizontal") >= 0.7f && !singlePulse3)
        {
            SkinMovement(true, 2);
            singlePulse3 = true;
        }
        if (Input.GetAxis("LeftJoystick3Horizontal") <= -0.7f && !singlePulse3)
        {
            SkinMovement(false, 2);
            singlePulse3 = true;
        }
        if (Input.GetAxis("LeftJoystick3Horizontal") == 0)
            singlePulse3 = false;
        #endregion
        #region Inputs Joystick 4
        if (Input.GetAxis("LeftJoystick4Horizontal") >= 0.7f && !singlePulse4)
        {
            SkinMovement(true, 3);
            singlePulse4 = true;
        }
        if (Input.GetAxis("LeftJoystick4Horizontal") <= -0.7f && !singlePulse4)
        {
            SkinMovement(false, 3);
            singlePulse4 = true;
        }
        if (Input.GetAxis("LeftJoystick4Horizontal") == 0)
            singlePulse4 = false;
        #endregion
        #region Players Are Ready
        if (Input.GetButtonDown("AButton1") && playerJoined[0])
            PlayersReady(0);
        if (Input.GetButtonDown("AButton2") && playerJoined[1])
            PlayersReady(1);
        if (Input.GetButtonDown("AButton3") && playerJoined[2])
            PlayersReady(2);
        if (Input.GetButtonDown("AButton4") && playerJoined[3])
            PlayersReady(3);
        #endregion
        #region Players Cancel
        if (Input.GetButtonDown("BButton1") && playerJoined[0])
            PlayersCancel(0);
        if (Input.GetButtonDown("BButton2") && playerJoined[1])
            PlayersCancel(1);
        if (Input.GetButtonDown("BButton3") && playerJoined[2])
            PlayersCancel(2);
        if (Input.GetButtonDown("BButton4") && playerJoined[3])
            PlayersCancel(3);
        #endregion
        if (Input.GetButtonDown("StartButton"))
            SceneManager.LoadScene("TestLevel");
    }
    public void PlayersReady (int playerNumber)
    {
        scriptablePlayers[playerNumber].actualSkin = skinsReference.skins[actualSkin[playerNumber]];
        playerReady[playerNumber] = true;
        skinsPlayers[playerNumber].GetComponent<VFX>().Score();
    }
    public void PlayersCancel (int playerNumber)
    {
        playerReady[playerNumber] = false;
        txtsToJoin[playerNumber].CrossFadeAlpha(1, 1, true);
        skinsPlayers[playerNumber].SetActive(false);
        playerJoined[playerNumber] = false;
    }
    public void PlayerJoin (int playerNumber)
    {
        switch (playerNumber)
        {
            case 0:
                playerJoined[playerNumber] = true;
                skinsPlayers[0].SetActive(true);
                txtsToJoin[0].CrossFadeAlpha(0, 1, true);
                break;
            case 1:
                playerJoined[playerNumber] = true;
                skinsPlayers[1].SetActive(true);
                txtsToJoin[1].CrossFadeAlpha(0, 1, true);
                break;
            case 2:
                playerJoined[playerNumber] = true;
                skinsPlayers[2].SetActive(true);
                txtsToJoin[2].CrossFadeAlpha(0, 1, true);
                break;
            case 3:
                playerJoined[playerNumber] = true;
                skinsPlayers[3].SetActive(true);
                txtsToJoin[3].CrossFadeAlpha(0, 1, true);
                break;
        }
    }
    public void SkinMovement (bool right, int playerNumber)
    {
        if (playerJoined[playerNumber])
        {
            if (right && actualSkin[playerNumber] < skinsPlayers.Length - 1)
                actualSkin[playerNumber]++;
            if (!right && actualSkin[playerNumber] >= 1)
                actualSkin[playerNumber]--;

            GameObject lastSkin = skinsPlayers[playerNumber];
            lastSkin.SetActive(false);
            skinsPlayers[playerNumber] = Instantiate(skinsReference.skins[actualSkin[playerNumber]], initialPos[playerNumber], Quaternion.identity);
            skinsPlayers[playerNumber].SetActive(true);
        }
    }

}
