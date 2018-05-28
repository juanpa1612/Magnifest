using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayersIndicatorOnline : Photon.PunBehaviour
{

    [SerializeField]
    Text[] playersTxt;
    [SerializeField]
    GameObject[] players;

    Vector3[] playersPositions;
    Vector3 offset;
    void Start ()
    {
        playersPositions = new Vector3[4];
        offset = new Vector3(0, 10, 0);

	}
	
	void Update ()
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = GameObject.Find("Scriptable Player " + (i + 1) + "(Clone)");
            }
        }
        for (int i = 0; i < playersPositions.Length; i++)
        {
            if (players[i] != null)
            {
                playersPositions[i] = players[i].transform.position;
            }
        }
        for (int i = 0; i < playersTxt.Length; i++)
        {
            if (players[i] != null)
            {
                playersTxt[i].transform.position = Camera.main.WorldToScreenPoint(playersPositions[i] + offset);
            }
        }
    }
}
