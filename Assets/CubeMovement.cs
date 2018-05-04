using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour {
    [SerializeField]
    Vector3 posB;
    Vector3 posA;
    Vector3 newPos;
    void Start ()
    {
        newPos = transform.position;
        posA = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newPos = posB;
        }
        if (Vector3.Distance(transform.position,posB) <= 0.2f)
        {
            newPos = posA;
        }
	}
}
