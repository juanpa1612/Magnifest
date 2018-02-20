using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    PlayerMovement playerMove;
    ChargingUI chargePlay;
    float r;
    float rDest;
    float tiempoDisparo;

	void Start () {
        playerMove = GetComponent<PlayerMovement>();
        chargePlay = GetComponent<ChargingUI>();
        tiempoDisparo = 0.4f;
        this.enabled = false;
	}
	

	void Update () {
        transform.position = new Vector3(r * Mathf.Cos(playerMove.w*playerMove.t), 0, r * Mathf.Sin(playerMove.w * playerMove.t));
        if (tiempoDisparo>0)
        {
            tiempoDisparo -= Time.deltaTime;
            r = Mathf.Lerp(r, rDest, tiempoDisparo);
        }
        else
        {
            tiempoDisparo = 0.4f;
            playerMove.enabled = true;
            chargePlay.enabled = true;
            this.enabled = false;
        }
    }

    public void PasarEstadoDisparo()
    {
        r = 0;
        chargePlay.enabled = false;
        rDest = playerMove.r;
    }
}
