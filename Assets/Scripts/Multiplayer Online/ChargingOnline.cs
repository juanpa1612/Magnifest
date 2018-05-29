using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingOnline : Photon.PunBehaviour
{

    [SerializeField] GameObject chargingArrow;
    [SerializeField] float fireSpeed;
    GameObject colliderBack;

    Center centerScript;
    GameObject center;

    PhotonView pv; 

    float penaltyTime;
    float arrowDirectX;
    float arrowDirectY;
    float chargingTime;
    bool penalized;
    public bool isCharging;
    bool fullyCharged;
    bool isFiring;
    bool backToPos;
    public bool canHit;
    public bool canBeHit;
    Vector3 joystickVector;
    Vector3 lastPos;
    Transform temp;
    PlayerMovementOnline playerMove;
    PlayerAudio playerAudio;
    VFX vfxReference;
    DeathScriptOnline deathScript;

    /*
    public bool IsCharging
    {
        get
        {
            return isCharging;
        }
    }
    */
    public float PenaltyTime
    {
        get
        {
            return penaltyTime;
        }
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        colliderBack = GameObject.Find("ColliderPlayer" + PhotonNetwork.player.ID);
       if(center == null)
        {
            center = GameObject.FindGameObjectWithTag("Center");
        }
        centerScript = center.GetComponent<Center>();
        playerAudio = GetComponent<PlayerAudio>();
        playerMove = GetComponent<PlayerMovementOnline>();
        deathScript = GetComponent<DeathScriptOnline>();
        vfxReference = GetComponentInChildren<VFX>();
        chargingTime = 0;
        penalized = false;
        joystickVector = Vector3.zero;
        penaltyTime = 0;
        isCharging = false;
        fullyCharged = false;
    }

    public void Reset()
    {
        chargingTime = 0;
        penalized = false;
        joystickVector = Vector3.zero;
        penaltyTime = 0;
        isCharging = false;
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
        if (!isCharging && !penalized && !isFiring && !fullyCharged)
        {
            playerMove.enabled = false;
            isCharging = true;
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
        if (isCharging && !fullyCharged)
        {
            backToPos = true;
            isCharging = false;
            penaltyTime = 3f;
            penalized = true;
            chargingArrow.SetActive(false);
            //playerAudio.StopSounds();
            vfxReference.StartChargingParticle(false);
			vfxReference.StartAuraParticles (true);

			vfxReference.StartAuraParticles (false);
        }
    }
    public void Update()
    {
        if (pv.isMine)
        {
            canHit = ICanHit();
            canBeHit = CanBeHit();
        }

        if (vfxReference == null)
        {
            vfxReference = GetComponentInChildren<VFX>();
        }
        if (pv.isMine)
        {
            if (penalized)
            {

                penaltyTime -= Time.deltaTime;
                if (penaltyTime <= 0)
                {
                    //vfxReference.StartAuraParticles (true);
                    penalized = false;
                }
            }
        }


        //Carga Incompleta
        if (backToPos)
        {
            playerAudio.FireSound();
            
            if (transform.position != colliderBack.transform.position)
            {
                transform.LookAt(colliderBack.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * fireSpeed);
            }
            

        }
        if (isCharging)
        {
            playerMove.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, 1.5f);
        }
        //Carga Completa
        if (pv.isMine)
        {
            if (gameObject.transform.position == center.transform.position)
            {
                colliderBack.transform.position = new Vector3(2000, 2000, 2000);
                isCharging = false;
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
            }
        }
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
        //if (collision.CompareTag("Player"))
            //canHit = ICanHit();
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
    public bool ICanHit ()
    {
        if (!playerMove.enabled && !isCharging && !deathScript.enabled)
            return true;
        else
            return false;
    }
    public bool CanBeHit()
    {
            if (!playerMove.Collided && !isCharging)
                return true;
            else
                return false;   
    }
    //PhotonView
    private void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            if (pv.isMine)
            {               
                stream.SendNext(canHit);
                stream.SendNext(isCharging);
                stream.SendNext(canBeHit);
            }
           
        }
        else
        {
            if (!pv.isMine)
            {               
                canHit = (bool)stream.ReceiveNext();
                isCharging = (bool)stream.ReceiveNext();
                canBeHit = (bool)stream.ReceiveNext();
            }          
        }
    }
}
