using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCamera : MonoBehaviour {

    public Transform camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float distance = (transform.position - camera.position).magnitude;
        float m = (0.1f - 2f) / (5 - 1);
        float fuerza = (m * distance) + (2f - (m * 1));

        if (distance > 5)
        {
            distance = 5;
        }
        if (distance < 1)
        {
            distance = 1;
        }

        Debug.Log(distance);

        GetComponent<Renderer>().material.SetFloat("_Fuerza", fuerza);

	}
}
