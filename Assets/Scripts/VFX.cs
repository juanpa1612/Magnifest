using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class VFX : MonoBehaviour
{

	void Start ()
    {
        PlayerMovement.onHit += ShakeCamera;
	}
	
	void Update ()
    {
		
	}
    public void ShakeCamera ()
    {
        CameraShaker.Instance.ShakeOnce(2f, 5f, .5f, 1f);
    }
}
