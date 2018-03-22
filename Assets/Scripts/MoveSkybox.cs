using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkybox : MonoBehaviour {

	public float rotateSpeed;

	void Update() 
	{

		RenderSettings.skybox.SetFloat ("_Rotation", rotateSpeed * Time.time);
		Debug.Log (RenderSettings.skybox.GetFloat("_Rotation"));
	}


}
