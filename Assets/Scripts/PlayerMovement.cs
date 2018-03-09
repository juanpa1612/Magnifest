using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float angularVelocity;
    public float radius;
    public float time;

    float radiusDestiny;
    bool singlePulsePad;
    bool direction;
    bool changeRing;
    float timeRingChange;

    [SerializeField] float valueIncRad;
    
    private bool collided;
    private float recoveryTime;

    [SerializeField]
    float timeRingMax;

    int lives;

    void Start ()
    {
        lives = 3;
        timeRingChange = timeRingMax;
        time = 0;
        radius = valueIncRad;
        angularVelocity = 2;
        singlePulsePad = false;
        direction = false;
        changeRing = false;
        radiusDestiny = 0;
        
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
    }

    void Update ()
    {
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
            timeRingChange -= Time.deltaTime;
            radius = Mathf.Lerp(radius, radiusDestiny, timeRingChange);
            if (timeRingChange<=0)
            {
                changeRing = false;
                timeRingChange=timeRingMax;
                radius=Mathf.Round(radius);
            }
        }
        
       
    }
    public void ChangeRing (bool addOrSub)
    {
        if (addOrSub && radius < 65 && !changeRing)
        {
            radiusDestiny = radius + valueIncRad;
            changeRing = true;
        }
        if (!addOrSub && radius > 18 && !changeRing)
        {
            radiusDestiny = radius - valueIncRad;
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
            if (collision.GetComponent<ChargingUI>().Charging == false && !collision.GetComponent<DeathScript>().enabled && !GetComponent<ChargingUI>().Charging)
            {
                Debug.Log("Me Chocaron");
                collided = true;
                radiusDestiny += collision.gameObject.GetComponent<PlayerMovement>().radius;
                changeRing = true;
                transform.rotation = collision.gameObject.transform.rotation;
                if (radiusDestiny > 68)
                {
                    GetComponent<DeathScript>().enabled = true;
                    GetComponent<ChargingUI>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}
