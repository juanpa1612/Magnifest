using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class DeathShakerTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//if # vidas jugador == 0, shakee la camara
		if (Input.GetMouseButtonDown (0)) {
			CameraShaker.Instance.ShakeOnce (2f, 5f, .5f, 1f);
		}
	}
}
