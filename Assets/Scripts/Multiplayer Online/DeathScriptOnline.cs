using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScriptOnline : Photon.PunBehaviour
{

    PlayerMovementOnline playerMove;
    PlayerAudio playerAudio;
    Transform Center;
    PlayersUIOnline playersUI;

    public delegate void RingOut();
    public static event RingOut isOut;

    bool subLive;
    private int state;
    float overChargeRespawnTime;


    public bool SubLife
    {
        get
        {
            return subLive;
        }
    }

    private void Start()
    {
        if (Center == null)
        {
            Center = GameObject.FindGameObjectWithTag("Center").transform;
        }
        overChargeRespawnTime = 2.5f;
        playerMove = gameObject.GetComponent<PlayerMovementOnline>();
        playerAudio = GetComponent<PlayerAudio>();
        playersUI = GameObject.Find("Players UI").GetComponent<PlayersUIOnline>();
    }

    public void OverChargeDeath()
    {
        playerAudio = GetComponent<PlayerAudio>();
        playerAudio.LostSound();
        transform.position = new Vector3(1000, 1000, 1000);
        state = 1;

    }

    public void CollisionDeath()
    {
        state = 2;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(state);
        }
        else
        {
            state = (int)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (state == 1)
        {
            overChargeRespawnTime -= Time.deltaTime;
            if (overChargeRespawnTime <= 0)
            {
                state = 3;
                overChargeRespawnTime = 2.5f;
            }
        }
        //Salir del Ring
        if (state == 2)
        {
            playerAudio.LostSound();
            transform.Translate(Vector3.forward * 5);
            if (Vector3.Distance(transform.position, Center.position) > 1500)
            {
                state = 3;
            }
            if (isOut != null)
                isOut();
        }
        //Situar Player
        else if (state == 3)
        {
            transform.position = new Vector3(1000, 1000, 1000);
            if (!SubLife)
            {
                playerMove.subLifes();
                subLive = true;
                playersUI.UILife(PhotonNetwork.player.ID-1, playerMove.GetLifes());
                
            }
            if (playerMove.GetLifes() > 0)
            {
                transform.position = new Vector3(17, 0, 0);
                playerMove.enabled = true;
                playerMove.Reset();
                state = 0;
                subLive = false;
                this.enabled = false;
            }
        }

    }
    
}

