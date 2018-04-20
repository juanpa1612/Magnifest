using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private int state;
    PlayerMovement playerMove;

    [SerializeField] Transform Center;
    [SerializeField] PlayersUI playersUI;
    public delegate void RingOut();
    public static event RingOut isOut;
    bool subLive;
    PlayerAudio playerAudio;
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
        overChargeRespawnTime = 2.5f;
        playerMove=gameObject.GetComponent<PlayerMovement>();
        playerAudio = GetComponent<PlayerAudio>();
        
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
                playersUI.UILife((int)gameObject.GetComponent<Players>().inputNumber, playerMove.GetLifes());
                
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

