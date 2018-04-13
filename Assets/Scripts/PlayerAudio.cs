using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    AudioSource source;
    [SerializeField] AudioClip[] channelingSound;
    [SerializeField] AudioClip[] collisionSound;
    [SerializeField] AudioClip[] ringChangeSound;
    [SerializeField] AudioClip[] fireSound;
    [SerializeField] AudioClip scoreSound;
    [SerializeField] AudioClip lostSound;
    public AnimationCurve volumeCurve;
    float evaluateVolume;
   
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
	}
	
    public void ChannelingSound()
    {
        bool isChanneling = false;
        for (int i = 0; i < channelingSound.Length; i++)
        {
            if (source.clip == channelingSound[i])
            {
                isChanneling = true;
            }
        }
        if (!isChanneling)
        {
            int indexChanneling;
            indexChanneling = Random.Range(0, channelingSound.Length);
            source.clip = channelingSound[indexChanneling];
            source.Play();
        }
    }   

    public void CollisionSound()
    {
        bool collidingSound = false;
        for (int i = 0; i < collisionSound.Length; i++)
        {
            if (source.clip == collisionSound[i])
            {
                collidingSound = true;
            }
        }
        if (!collidingSound)
        {
            int indexCollision;
            indexCollision = Random.Range(0, collisionSound.Length);
            source.clip = collisionSound[indexCollision];
            source.Play();
        }
    }

    public void RingChangeSound(float timeRingMax)
    {
            int indexRingChange;
            indexRingChange = Random.Range(0, ringChangeSound.Length);
            source.clip = ringChangeSound[indexRingChange];
            source.time = 0.2f;
            source.Play();
        
    }

    public void StopSounds()
    {
        source.Stop();
    }
    public void FireSound ()
    {
        bool isFiring = false;
        for (int i = 0; i < fireSound.Length; i++)
        {
            if (source.clip == fireSound[i])
            {
                isFiring = true;
            }
        }
        if (!isFiring) { 
            int indexFireRange;
            indexFireRange = Random.Range(0, fireSound.Length);
            source.clip = fireSound[indexFireRange];
            source.Play();
        }
    }
    public void ScoreSound()
    {
        if (source.clip != scoreSound)
        {
            source.clip = scoreSound;
            source.Play();
        }
    }
    public void LostSound()
    {
        if (source.clip!=lostSound)
        {
            source.clip = lostSound;
            source.Play();
        }
    }
    // Update is called once per frame
    void Update ()
    {
        /*
        if (source.clip != null && source.isPlaying)
        {
            evaluateVolume += Time.deltaTime;
            source.volume = volumeCurve.Evaluate(evaluateVolume / source.clip.length);
        }
        
        if (!source.isPlaying)
        {
            evaluateVolume = 0;
        }
        */
	}
}
