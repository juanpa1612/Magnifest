using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup main;
    [SerializeField]
    CanvasGroup skinSelection;
    [SerializeField]
    GameObject playersSkins;

    bool start;
    Vector3 posB;

	void Start ()
    {
        posB = new Vector3(0, 0, -60);
        skinSelection.alpha = 0;
	}
	

	void Update ()
    {
        if (!start)
        {
            if (Input.anyKeyDown)
                start = true;
        }
        if (start)
        {
            main.alpha -= Time.deltaTime;
            skinSelection.alpha += Time.deltaTime;
            playersSkins.transform.position = Vector3.Lerp(playersSkins.transform.position, posB, 0.05f);
        }
	}
}
