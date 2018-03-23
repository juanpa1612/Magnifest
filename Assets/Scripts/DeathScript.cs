using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private int state;
    PlayerMovement playerMove;

    [SerializeField] Transform Center;

    public delegate void RingOut();
    public static event RingOut isOut;
    bool subLive;
    private void Start()
    {
        state = 2;
        playerMove=gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Salir del Ring
        if (state == 2)
        {
            transform.Translate(Vector3.forward * 5);
            if (isOut != null)
                isOut();
        }
        //Situar Player
        else if (state == 3)
        {
            transform.position = new Vector3(1000, 1000, 1000);
            if (!subLive)
            {
                playerMove.subLives();
                subLive = true;
            }
            if (playerMove.GetLives() > 0)
            {
                transform.position = new Vector3(17, 0, 0);
                playerMove.enabled = true;
                playerMove.Reset();
                state = 2;
                subLive = false;
                this.enabled = false;
            }
        }

        if(Vector3.Distance(transform.position, Center.position)>1500)
        {
            state = 3;
        }

    }
    
}

