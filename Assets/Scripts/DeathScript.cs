using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private int estado;

    private void Start()
    {
        estado = 2;
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
        }

    }
    void OnBecomeInvisible()
    {
        estado = 3;
    }
}

