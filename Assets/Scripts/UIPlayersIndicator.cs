using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayersIndicator : MonoBehaviour {

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
        playersPositions[0] = players[0].transform.position;
        playersPositions[1] = players[1].transform.position;
        playersPositions[2] = players[2].transform.position;
        playersPositions[3] = players[3].transform.position;

        playersTxt[0].transform.position = Camera.main.WorldToScreenPoint(playersPositions[0] + offset);
        playersTxt[1].transform.position = Camera.main.WorldToScreenPoint(playersPositions[1] + offset);
        playersTxt[2].transform.position = Camera.main.WorldToScreenPoint(playersPositions[2] + offset);
        playersTxt[3].transform.position = Camera.main.WorldToScreenPoint(playersPositions[3] + offset);
    }
}
