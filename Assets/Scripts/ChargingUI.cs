using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUI : MonoBehaviour
{
    [SerializeField] GameObject center;
    [SerializeField] GameObject chargingArrow;
    [SerializeField] float fireSpeed;
    [SerializeField] GameObject colliderBack;

    Center centerScript;

    float penaltyTime;
    float arrowDirectX;
    float arrowDirectY;
    float chargingTime;
    bool penalized;
    bool charging;
    bool fullyCharged;
    bool isFiring;
    bool backToPos;

    Vector3 joystickVector;
    Vector3 lastPos;
    Transform temp;
    PlayerMovement playerMove;
    PlayerAudio playerAudio;
    VFX vfxReference;
    DeathScript deathScript;

    public bool Charging
    {
        get
        {
            return charging;
        }
    }
    public float PenaltyTime
    {
        get
        {
            return penaltyTime;
        }
    }

    private void Start()
    {
        centerScript = center.GetComponent<Center>();
        playerAudio = GetComponent<PlayerAudio>();
        playerMove = GetComponent<PlayerMovement>();
        deathScript = GetComponent<DeathScript>();
        chargingTime = 0;
        penalized = false;
        joystickVector = Vector3.zero;
        penaltyTime = 0;
        charging = false;
        fullyCharged = false;
    }

    public void Reset()
    {
        chargingTime = 0;
        penalized = false;
        joystickVector = Vector3.zero;
        penaltyTime = 0;
        charging = false;
        fullyCharged = false;
        backToPos = false;
        isFiring = false;
        vfxReference.StartChargingParticle(false);
    }

    public float GetChargingTime()
    {
        return Mathf.Round(chargingTime);
    }
    public void StartCharging ()
    {
        if (!charging && !penalized && !isFiring && !fullyCharged)
        {
            playerMove.enabled = false;
            charging = true;
            //lastPos = transform.position;
            colliderBack.transform.position = transform.position;
            playerAudio.ChannelingSound();
            vfxReference.StartChargingParticle(true);
            vfxReference.StartShootingParticle(false);
			vfxReference.StartAuraParticles (false);
        }
    }
    public void StopCharging ()
    {
        if (charging && !fullyCharged)
        {
            backToPos = true;
            charging = false;
            penaltyTime = 3f;
            penalized = true;
            chargingArrow.SetActive(false);
            //playerAudio.StopSounds();
            vfxReference.StartChargingParticle(false);
			vfxReference.StartAuraParticles (true);

			vfxReference.StartAuraParticles (false);
        }
    }
    private void Update()
    {
        if (vfxReference == null)
        {
            vfxReference = GetComponentInChildren<VFX>();
        }
        if (penalized)
        {
			
            penaltyTime -= Time.deltaTime;
            if (penaltyTime <= 0)
            {		
				//vfxReference.StartAuraParticles (true);
                penalized = false;
            }
        }

        //Carga Incompleta
        if (backToPos)
        {
            /*
            if (transform.position != lastPos)
            {
                transform.LookAt(lastPos);
                transform.Translate(Vector3.forward * Time.deltaTime * fireSpeed);
            }
            if (Vector3.Distance(transform.position, lastPos) < 0.5f)
            {
                transform.position = lastPos;
                playerMove.enabled = true;
                backToPos = false;
                vfxReference.StartAuraParticles(true);
            }
            */
            playerAudio.FireSound();
            
            if (transform.position != colliderBack.transform.position)
            {
                transform.LookAt(colliderBack.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * fireSpeed);
            }
            

        }
        if (charging)
        {
            playerMove.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, 1.5f);
        }
        //Carga Completa
        if (gameObject.transform.position == center.transform.position)
        {
            colliderBack.transform.position = new Vector3(2000, 2000, 2000);
            charging = false;
            fullyCharged = true;
            chargingArrow.SetActive(true);
            
            if (chargingTime < 3.5f)
            {
                chargingTime += Time.deltaTime;
            }
            else
            {
                deathScript.enabled = true;
                deathScript.OverChargeDeath();
                //fullyCharged = false;
                chargingArrow.SetActive(false);
                //centerScript.SetBusy(false);
                chargingTime = 0;
                this.enabled = false;
            }
        }/*
        else if (Vector3.Distance(transform.position,center.transform.position)<0.2f)
        {
            Debug.Log("Entro1");
            if (centerScript.GetBusy())
            {
                StopCharging();
                Debug.Log("Entro2");
            }
        }
        */
        if (fullyCharged && !isFiring)
        {
            
            //Apuntar
                if (arrowDirectX != 0 || arrowDirectY != 0)
                {
                    float angulo = Mathf.Atan2(arrowDirectX, arrowDirectY) * Mathf.Rad2Deg;
                    joystickVector.y = angulo;
                    gameObject.transform.eulerAngles = joystickVector;
                }
        }
        //Fire
        if (isFiring)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * fireSpeed);
            chargingArrow.SetActive(false);
            vfxReference.StartChargingParticle(false);
            vfxReference.StartShootingParticle(true);
            playerAudio.FireSound();
        }
    }
    public void Fire ()
    {
        if (fullyCharged)
        {
            centerScript.SetBusy(false);
            isFiring = true;
        }
            
    }
    public void ArrowDirection (float x, float y)
    {
        arrowDirectX = x * -1;
        arrowDirectY = y * -1;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isFiring && collision.gameObject.CompareTag("LastRing"))
        {
            fullyCharged = false;
            isFiring = false;
            playerMove.enabled = true;
            playerMove.radius = 68;
            chargingTime = 0;
            playerMove.time = (Mathf.Atan2(transform.position.z, transform.position.x) / 2);
            penaltyTime = 3f;
            penalized = true;
            vfxReference.StartShootingParticle(false);
			vfxReference.StartAuraParticles (true);
        }
        if (collision.CompareTag("Center"))
        {
            if (centerScript.GetBusy())
            {
                StopCharging();
            }
            else
            {
                centerScript.SetBusy(true);
            }
        }
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("ColliderBack") && collision.gameObject == colliderBack)
        {
            if (backToPos)
            {
                backToPos = false;
                transform.position = colliderBack.transform.position;
                colliderBack.transform.position = new Vector3(2000, 2000, 2000);
                playerMove.enabled = true;
                vfxReference.StartAuraParticles(true);
            }
        }
    }
}
