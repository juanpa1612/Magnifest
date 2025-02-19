﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTest : Photon.PunBehaviour
{

    [SerializeField] ScriptablePlayer[] players;
    [SerializeField]
    PlayerSelectionOnline pSelection;
    [SerializeField]
    MainMenu mainMenu;

	void Start ()
    {
        PhotonNetwork.sendRate = 180;
        PhotonNetwork.sendRateOnSerialize = 120;

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
        mainMenu.MenuAlpha();
        pSelection.PlayerJoin(PhotonNetwork.player.ID-1);

        //PhotonNetwork.Instantiate("Scriptable Player " + PlayerSelectionOnline.onlineNumber, new Vector3 (17, 0, 0), Quaternion.identity, 0);
        ////players[PhotonNetwork.playerList.Length].actualSkin = pSelection.skinsReference.skins[pSelection.actualSkin[1]];
        //PhotonNetwork.Instantiate("Cube", new Vector3(-48, 5, 0), Quaternion.identity, 0);
    }

}
