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

    void Start ()
    {
        slidersPlayers[0].maxValue = 3;
        slidersPlayers[1].maxValue = 3;
    }
	

	void Update ()
    {
		slidersPlayers[0].value = chargingP1.TiempoCastigo;
        slidersPlayers[1].value = chargingP2.TiempoCastigo;
    }
}
