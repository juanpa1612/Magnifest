using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Text txtLifes;
    [SerializeField]
    Text youLost;
    [SerializeField]
    Vector3 posFinal;

    int vidasPlayer1;

    Vector3 posB;

	void Start ()
    {
        vidasPlayer1 = 3;
        txtLifes.text = "Lifes: " + 3;
        posB = youLost.transform.localPosition;
    }
    private void Update()
    {
        youLost.transform.localPosition = Vector3.Lerp(youLost.transform.localPosition, posB, 0.05f);

        if (Input.GetKeyDown(KeyCode.H))
            PlayerOut();
    }
    public void LoseLife (int playerLifes)
    {
        if (playerLifes > 0)
            playerLifes--;
        if (playerLifes == 0)
            PlayerOut();

        txtLifes.text = "Lifes: " + playerLifes;
    }
    
    public void PlayerOut ()
    {
        posB = posFinal;
    }

    #region Singleton

    private static GameController instance;
    private GameController() { }

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameController.Instance es nula pero se esta intentando accederla");
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
                DestroyImmediate(this.gameObject);
        }
    }

    #endregion
}
