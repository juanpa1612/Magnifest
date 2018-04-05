using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float angularVelocity;
    public float radius;
    public float time;

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

    int lives;

    float startTime;
    float timeOnTransition;
    ChargingUI chargingUI;
    PlayerAudio playerAudio;

    public delegate void HitAction();
    public static event HitAction onHit;

    void Start()
    {
        
        timeOnTransition = 0;
        percentageRingChange = 0;
        lives = 3;
        percentageRingChange = timeRingMax;
        time = 0;
        //radius = valueIncRad;
        angularVelocity = 2;
        singlePulsePad = false;
        direction = false;
        changeRing = false;
        radiusDestiny = 0;
        chargingUI = GetComponent<ChargingUI>();

		playerAudio = GetComponent<PlayerAudio> ();
	}

    public int GetLives()
    {
        return lives;
    }

    public void subLives()
    {
        lives--;
    }

    public void Reset()
    {
        radius = 17;
        time = 0;
        changeRing = false;
        radiusDestiny = 0;
        chargingUI.enabled = true;
    }

    void Update ()
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
		playerAudio.RingChangeSound ();
        if (addOrSub && radius < 65 && !changeRing)
        {
            radiusDestiny = radius + valueIncRad;
            radiusOrigin = radius;
            startTime = Time.time;
            changeRing = true;
        }
        if (!addOrSub && radius > 18 && !changeRing)
        {
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
        if (collision.CompareTag("Player") && !collided && (!collision.GetComponent<PlayerMovement>().enabled))
        {
            if (collision.GetComponent<ChargingUI>().Charging == false && !collision.GetComponent<DeathScript>().enabled 
                && !chargingUI.Charging)
            {
                collided = true;
                playerAudio.CollisionSound();
                radiusDestiny += collision.gameObject.GetComponent<PlayerMovement>().radius;
                radiusOrigin = radius;
                startTime = Time.time;
                changeRing = true;
                //transform.rotation = collision.gameObject.transform.rotation;
                if (radiusDestiny > 68)
                {
                    GetComponent<DeathScript>().enabled = true;
                    chargingUI.enabled = false;
                    collision.GetComponentInChildren<VFX>().Score();
                    collision.GetComponent<PlayerAudio>().ScoreSound();
                    //playerAudio.LostSound();
                    this.enabled = false;
                }
                //Camera Shake
                if (onHit != null)
                    onHit();
            }
        }
    }
}
