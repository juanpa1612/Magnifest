using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayers : Photon.PunBehaviour
{
    Vector3[] spawnPositions;
    [SerializeField] GameControllerOnline gcOnline;
	void Start ()
    {
        spawnPositions = new Vector3[4];
        spawnPositions[0] = new Vector3(17, 0, 0);
        spawnPositions[1] = new Vector3(34, 0, 0);
        spawnPositions[2] = new Vector3(51, 0, 0);
        spawnPositions[3] = new Vector3(68, 0, 0);

        PhotonNetwork.Instantiate("Scriptable Player " + PhotonNetwork.player.ID, 
        spawnPositions[PhotonNetwork.player.ID], Quaternion.identity, 0);
        gcOnline.GetComponent<PhotonView>().RPC("AddPlayerToList", PhotonTargets.All, PhotonNetwork.player.ID);
        //gcOnline.AddPlayerToList(PhotonNetwork.player.ID);
    }
	

	void Update ()
    {
		
	}
}
