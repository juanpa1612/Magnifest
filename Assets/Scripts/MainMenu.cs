using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup main;
    
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
        if (!startReady)
        {
            if (Input.anyKeyDown)
                startReady = true;
        }
        if (startReady)
        {
            main.alpha -= Time.deltaTime;
            skinSelection.alpha += Time.deltaTime;
        }
	}
}
