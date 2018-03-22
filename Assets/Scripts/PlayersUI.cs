using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersUI : MonoBehaviour
{
    [SerializeField]
    Slider[] slidersPlayers;


    [SerializeField]
    ChargingUI chargingP1;
    [SerializeField]
    ChargingUI chargingP2;
    [SerializeField]
    ChargingUI chargingP3;
    [SerializeField]
    ChargingUI chargingP4;

    void Start ()
    {
        slidersPlayers[0].maxValue = 3;
        slidersPlayers[1].maxValue = 3;
        slidersPlayers[2].maxValue = 3;
        slidersPlayers[3].maxValue = 3;
    }
	

	void Update ()
    {
		slidersPlayers[0].value = chargingP1.PenaltyTime;
        slidersPlayers[1].value = chargingP2.PenaltyTime;
        slidersPlayers[2].value = chargingP3.PenaltyTime;
        slidersPlayers[3].value = chargingP4.PenaltyTime;
    }
}
