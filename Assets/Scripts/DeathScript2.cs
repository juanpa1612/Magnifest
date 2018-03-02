using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript2 : MonoBehaviour
{
    private int estado;
    PlayerMovement2 playerMove2;

    private void Start()
    {
        estado = 2;
        playerMove2=gameObject.GetComponent<PlayerMovement2>();
    }

    void Update ()
    {
        if (estado == 2)
        {
            transform.Translate(-Vector3.forward * 5);
        }
        else if (estado == 3)
        {
            transform.position = new Vector3(1000, 1000, 1000);
            if (playerMove2.GetVidas()>0)
            {
                transform.position = new Vector3(17, 0, 0);
                playerMove2.enabled = true;
                playerMove2.Reset();
                playerMove2.RestarVidas();
                this.enabled = false;
            }
        }

        if(Vector3.Distance(transform.position, new Vector3(0, 0, 0))>1000)
        {
            estado = 3;
        }

    }
    
}

