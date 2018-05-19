using UnityEngine;
using System;
public class PlayerMovementOnline : Photon.PunBehaviour
{

    
    public float radius;
    public float time;

    float angularVelocity;
    float radiusDestiny;
    float radiusOrigin;
    bool singlePulsePad;
    bool direction;
    bool changeRing;
    float percentageRingChange;

    [SerializeField] float valueIncRad;
    
    private bool collided;
    private float recoveryTime;

    [SerializeField]
    float timeRingMax;

    int lifes;
    bool collidable;
    float startTime;
    float timeOnTransition;
    ChargingOnline chargingOnline;
    PlayerAudio playerAudio;
    DeathScriptOnline deathScriptOnline;
    public delegate void HitAction();
    public static event HitAction onHit;

    void Start()
    {
        
        timeOnTransition = 0;
        percentageRingChange = 0;
        lifes = 3;
        percentageRingChange = timeRingMax;
        time = 0;
        //radius = valueIncRad;
        angularVelocity = 2;
        singlePulsePad = false;
        direction = false;
        changeRing = false;
        radiusDestiny = 0;
        chargingOnline = GetComponent<ChargingOnline>();
		playerAudio = GetComponent<PlayerAudio> ();
        deathScriptOnline = GetComponent<DeathScriptOnline>();

        if (photonView.isMine)
        {
            int actualSkinIndex = Array.IndexOf(GetComponent<PlayersOnline>().Player.skinsReference.skins,
                GetComponent<PlayersOnline>().Player.actualSkin);

            GetComponent<PhotonView>().RPC("UpdateSkins", PhotonTargets.All, PhotonNetwork.player.ID, actualSkinIndex);
        }
	}
    [PunRPC]
    public void UpdateSkins(int playerID,int actualSkinIndex)
    {
        GameObject playerToUpdate = GameObject.Find("Scriptable Player " + playerID + "(Clone)");

        playerToUpdate.GetComponent<PlayersOnline>().Player.actualSkin = 
            GetComponent<PlayersOnline>().Player.skinsReference.skins[actualSkinIndex];
    }
    public int GetLifes()
    {
        return lifes;
    }

    public void subLifes()
    {
        lifes--;
    }

    public void Reset()
    {
        radius = 17;
        time = 0;
        changeRing = false;
        radiusDestiny = 0;
        chargingOnline.enabled = true;
        chargingOnline.Reset();
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(radius);
            stream.SendNext(time);
            stream.SendNext(radiusDestiny);
            stream.SendNext(collidable);
        }
        else
        {
            radius = (float)stream.ReceiveNext();
            time = (float)stream.ReceiveNext();
            radiusDestiny = (float)stream.ReceiveNext();
            collidable = (bool)stream.ReceiveNext();
        }
    }
    void Update()
    {
        transform.LookAt(2 * transform.position - Vector3.zero);
        transform.position = new Vector3(Mathf.Cos(angularVelocity * time), 0, Mathf.Sin(angularVelocity * time)) * radius;
        //Collision
        if (collided)
        {
            recoveryTime -= Time.deltaTime;
            if (recoveryTime <= 0)
            {
                collided = false;
                recoveryTime = 2f;
            }
        }
        if (!direction)
        {
            time += Time.deltaTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
        
        if (changeRing)
        {
            timeOnTransition = Time.time - startTime;
            percentageRingChange = timeOnTransition / timeRingMax; 
            radius = Mathf.Lerp(radiusOrigin, radiusDestiny, percentageRingChange);
            if (timeOnTransition>=timeRingMax)
            {
                changeRing = false;
                percentageRingChange=0;
                timeOnTransition = 0;
                radius=Mathf.Round(radius);
            }
        }
        
    }
    public void ChangeRing (bool addOrSub)
    {

        if (addOrSub && radius < 65 && !changeRing)
        {
            playerAudio.RingChangeSound(timeRingMax);
            radiusDestiny = radius + valueIncRad;
            radiusOrigin = radius;
            startTime = Time.time;
            changeRing = true;
        }
        if (!addOrSub && radius > 18 && !changeRing)
        {
            playerAudio.RingChangeSound(timeRingMax);
            radiusDestiny = radius - valueIncRad;
            radiusOrigin = radius;
            startTime = Time.time;
            changeRing = true;
        }
    }
    public void ChangeDirection (bool right)
    {
        if (right)
            direction = true;
        else
            direction = false;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collidable = NetworkCollision();
            if (collision.GetComponent<ChargingOnline>().ICanHit() && collidable)
            {
                collided = true;
                playerAudio.CollisionSound();
                radiusOrigin = radius;
                radiusDestiny = collision.gameObject.GetComponent<PlayerMovementOnline>().radius + radius;
                startTime = Time.time;
                changeRing = true; ;
                if (radiusDestiny > 68)
                {
                    deathScriptOnline.enabled = true;
                    deathScriptOnline.CollisionDeath();
                    chargingOnline.enabled = false;
                    collision.GetComponentInChildren<VFX>().Score();
                    collision.GetComponent<PlayerAudio>().ScoreSound();
                    //playerAudio.LostSound();
                    this.enabled = false;
                }
                //Camera Shake & Control Vibration
                if (onHit != null)
                    onHit();
            }
        }
    }
    public bool NetworkCollision ()
    {
        if (!collided && !chargingOnline.IsCharging)
        {
            return true;
        }
        return false;
    }
}
