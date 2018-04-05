using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class VFX : MonoBehaviour
{
    ParticleSystem scoreParticle;
    ParticleSystem chargeParticle;
    ParticleSystem shootTrail;
    ParticleSystem[] particles;

	void Start ()
    {
        PlayerMovement.onHit += ShakeCamera;
        //DeathScript.isOut += Score;
        particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (var item in particles)
        {
            if (item.name == "Score")
                scoreParticle = item;
            if (item.name == "Charge")
                chargeParticle = item;
            if (item.name == "ShootTrail")
                shootTrail = item;
        }
	}
	
	void Update ()
    {
		
	}
    public void StartChargingParticle (bool state)
    {
        if (state)
            chargeParticle.Play();
        if (!state)
            chargeParticle.Stop();
    }

    public void StartShootingParticle (bool state)
    {
        if (state)
            shootTrail.Play();
        if (!state)
            shootTrail.Stop();

    }
    public void Score ()
    {
        if (scoreParticle)
            scoreParticle.Play();

    }
    public void ShakeCamera ()
    {
        CameraShaker.Instance.ShakeOnce(2f, 5f, .5f, 1f);
    }
}
