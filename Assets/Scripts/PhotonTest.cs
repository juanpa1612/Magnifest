using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTest : Photon.PunBehaviour
{


	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("Alpha_V01");
	}
    public override void OnConnectedToPhoton()
    {
        Debug.Log("Me he conectado a photon");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Un usuario se ha unido al servidor");
        var rOptions = new RoomOptions();
        rOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("TestRoom", rOptions, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Cuarto creado");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Me uní al cuarto");
    }
}
