using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup main;
    [SerializeField]
    GameObject[] initialSkins;
    public CanvasGroup skinSelection;
    bool startReady;
    Vector3 posB;

    public bool StartReady
    {
        get
        {
            return startReady;
        }
    }

    void Start ()
    {
        posB = new Vector3(0, 0, -60);
        skinSelection.alpha = 0;
        main.alpha = 1;
	}
	

	void Update ()
    {

        if (startReady)
        {
            main.alpha -= Time.deltaTime;
            skinSelection.alpha += Time.deltaTime;
        }
	}
    public void MenuAlpha ()
    {
        startReady = true;
    }
}
