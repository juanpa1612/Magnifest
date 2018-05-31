using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersUIOnline : MonoBehaviour
{
    [SerializeField]
    Slider[] slidersPlayers;
    [SerializeField]
    Image[] chargingImages;
    [SerializeField]
    ChargingOnline[] chargingScripts;
    [SerializeField] GameObject[] uiLifes;
    float maxPenaltyTime = 3f;

    [SerializeField] GameObject[] players;

    DeathScriptOnline[] deathScripts;
    Vector3[] playersPosition;
    Vector3 offset;
    void Start ()
    {
        //slidersPlayers[0].maxValue = 3;
        //slidersPlayers[1].maxValue = 3;
        //slidersPlayers[2].maxValue = 3;
        //slidersPlayers[3].maxValue = 3;
        playersPosition = new Vector3[4];
        offset = new Vector3(0, 10, 0);
        deathScripts = new DeathScriptOnline[4];
        chargingScripts = new ChargingOnline[4];

    }

    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                deathScripts[i] = players[i].GetComponent<DeathScriptOnline>();
            }
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = GameObject.Find("Scriptable Player " + (i + 1) + "(Clone)");
            }
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] != null)
            {
                playersPosition[i] = players[i].transform.position;
            }
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] != null)
            {
                chargingImages[i].transform.position = Camera.main.WorldToScreenPoint(playersPosition[i] + offset);
            }
        }
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] != null)
            {
                chargingScripts[i] = players[i].GetComponent<ChargingOnline>();
            }
        }
        //slidersPlayers[0].value = chargingP1.PenaltyTime;
        //slidersPlayers[1].value = chargingP2.PenaltyTime;
        //slidersPlayers[2].value = chargingP3.PenaltyTime;
        //slidersPlayers[3].value = chargingP4.PenaltyTime;
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] != null)
            {
                chargingImages[i].fillAmount = chargingScripts[i].PenaltyTime / maxPenaltyTime;
            }
        }
    }

    public void UILife(int player, int lifes)
    {
        uiLifes[player].GetComponentsInChildren<Image>()[lifes].gameObject.SetActive(false);
    }
}
