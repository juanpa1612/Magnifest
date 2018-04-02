using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    AudioSource source;
    [SerializeField] AudioClip[] channelingSound;
    [SerializeField] AudioClip[] collisionSound;
    [SerializeField] AudioClip[] ringChangeSound;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
	}
	
    public void ChannelingSound()
    {
        int indexChanneling;
        indexChanneling = Random.Range(0, channelingSound.Length);
        source.clip = channelingSound[indexChanneling];
        source.loop = true;
        source.Play();
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

    // Update is called once per frame
    void Update () {
		
	}
}
