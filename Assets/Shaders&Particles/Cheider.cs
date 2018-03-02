using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheider : MonoBehaviour {

    public Material m;

	// Use this for initialization
	void Start () {
        m = GetComponentInChildren<SkinnedMeshRenderer>().material;
	} 
	


	// Update is called once per frame
	void Update () {

        m.SetFloat("_Mezclador", transform.localScale.magnitude);
	}
}
