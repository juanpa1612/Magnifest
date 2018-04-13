using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersUI : MonoBehaviour
{
    [SerializeField]
    Slider[] slidersPlayers;
    [SerializeField]
    Image[] chargingImages;

    [SerializeField]
    ChargingUI chargingP1;
    [SerializeField]
    ChargingUI chargingP2;
    [SerializeField]
    ChargingUI chargingP3;
    [SerializeField]
    ChargingUI chargingP4;
    float maxPenaltyTime = 3f;

    [SerializeField] GameObject[] players;
    Vector3[] playersPosition;
    Vector3 offset;
    void Start ()
    {
        slidersPlayers[0].maxValue = 3;
        slidersPlayers[1].maxValue = 3;
        slidersPlayers[2].maxValue = 3;
        slidersPlayers[3].maxValue = 3;
        playersPosition = new Vector3[4];
        offset = new Vector3(0, 10, 0);
    }
	

	void Update ()
    {
		slidersPlayers[0].value = chargingP1.PenaltyTime;
        slidersPlayers[1].value = chargingP2.PenaltyTime;
        slidersPlayers[2].value = chargingP3.PenaltyTime;
        slidersPlayers[3].value = chargingP4.PenaltyTime;

        playersPosition[0] = players[0].transform.position;
        playersPosition[1] = players[1].transform.position;
        playersPosition[2] = players[2].transform.position;
        playersPosition[3] = players[3].transform.position;

        chargingImages[0].transform.position = Camera.main.WorldToScreenPoint(playersPosition[0] + offset);
        chargingImages[1].transform.position = Camera.main.WorldToScreenPoint(playersPosition[1] + offset);
        chargingImages[2].transform.position = Camera.main.WorldToScreenPoint(playersPosition[2] + offset);
        chargingImages[3].transform.position = Camera.main.WorldToScreenPoint(playersPosition[3] + offset);

        chargingImages[0].fillAmount = chargingP1.PenaltyTime/maxPenaltyTime;
        chargingImages[1].fillAmount = chargingP2.PenaltyTime / maxPenaltyTime;
        chargingImages[2].fillAmount = chargingP3.PenaltyTime / maxPenaltyTime;
        chargingImages[3].fillAmount = chargingP4.PenaltyTime / maxPenaltyTime;
    }
}
