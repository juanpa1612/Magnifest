using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private int estado;
    PlayerMovement playerMove;

    private void Start()
    {
        estado = 2;
        playerMove=gameObject.GetComponent<PlayerMovement>();
    }

    void Update ()
    {
        //Salir del Ring
        if (estado == 2)
        {
            transform.Translate(-Vector3.forward * 5);
        }
        //Situar Player
        else if (estado == 3)
        {
            transform.position = new Vector3(1000, 1000, 1000);
            if (playerMove.GetVidas()>0)
            {
                transform.position = new Vector3(17, 0, 0);
                playerMove.enabled = true;
                playerMove.Reset();
                playerMove.RestarVidas();
                this.enabled = false;
            }
        }

        if(Vector3.Distance(transform.position, new Vector3(0, 0, 0))>1000)
        {
            estado = 3;
        }

    }
    
}

