using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public PathTrail actualPath;
    public PathTrail outsidePath;
    public PathTrail insidePath;
    public float speed;


    private Vector3 centroCarga;
    private Vector3 centroCargaOffset;
    private int currentSegment;
    private float transition;
    private bool isCompleted;
    private bool forward = true;
    private bool cargando;

    private void Start()
    {
        actualPath = outsidePath;
    }

    private void Update()
    {
        if (!actualPath)
            return;

        if (!isCompleted && forward && !cargando)
            Play();

        else if (!isCompleted && !forward)
            ReversePlay();

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ChangePath();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && forward)
        {
            transition = 1 - transition;
            forward = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !forward)
        {
            transition = 1 - transition;
            forward = true;
        }

    }

    private void ReversePlay()
    {
        transition += Time.deltaTime / speed;

        if (transition > 1)
        {
            transition = 0;
            currentSegment--;
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSegment++;
        }

        transform.position = actualPath.LinearPositionBack(currentSegment, transition);
    }
    private void Play ()
    {
        transition += Time.deltaTime / speed;

        if (transition > 1)
        {
            transition = 0;
            currentSegment++;
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSegment--;
        }

        transform.position = actualPath.LinearPosition(currentSegment, transition);
        //transform.rotation = actualPath.Orientation(currentSegment, transition);
    }

    public void ChangePath ()
    {
        if (actualPath == outsidePath)
        {
            actualPath = insidePath;
        }
        else
            actualPath = outsidePath;
    }
}
