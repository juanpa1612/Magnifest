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
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
	}
	
    public void ChannelingSound()
    {
        if (!source.isPlaying)
        {
            int indexChanneling;
            indexChanneling = Random.Range(0, channelingSound.Length);
            source.clip = channelingSound[indexChanneling];
            //source.loop = true;
            source.Play();
        }
    }

    public void CollisionSound()
    {
        int indexCollision;
        indexCollision = Random.Range(0, collisionSound.Length);
        source.clip = collisionSound[indexCollision];
        source.Play();
    }

    public void RingChangeSound()
    {
        int indexRingChange;
        indexRingChange = Random.Range(0, ringChangeSound.Length);
        source.clip = ringChangeSound[indexRingChange];
        source.Play();
    }

    public void StopSounds()
    {
        source.Stop();
    }
    public void FireSound ()
    {
        if (!source.isPlaying)
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
                //source.PlayOneShot(fireSound[indexFireRange], 1f);
                source.Play();
            }
        }
    }
    public void ScoreSound()
    {
        source.clip = scoreSound;
        source.Play();
    }
    public void LostSound()
    {
        if (source.isPlaying == false&&source.clip!=lostSound)
        {
            source.clip = lostSound;
            source.Play();
        }
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
