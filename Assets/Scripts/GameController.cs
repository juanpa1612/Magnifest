using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Text onLostText;
    [SerializeField]
    Vector3 finalPos;

    [SerializeField]
    Image endGameImage;

    [SerializeField]
    Text finalText;

    GameObject [] players;

    Vector3 posB;

	void Start ()
    {
        posB = onLostText.transform.localPosition;
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
       

        if (Input.GetKeyDown(KeyCode.H))
            PlayerOut();

        for(int i = 0; i<players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>() != null)
            {
                if (players[i].GetComponent<PlayerMovement>().GetLives()<=0)
                {
                    endGameImage.gameObject.SetActive(true);
                    finalText.text = "Player 2 Wins!";
                    Time.timeScale = 0;
                }
            }
            else if(players[i].GetComponent<PlayerMovement>() != null)
            {
                if (players[i].GetComponent<PlayerMovement>().GetLives() <= 0)
                {
                    endGameImage.gameObject.SetActive(true);
                    finalText.text = "Player 1 Wins!";
                    Time.timeScale = 0;
                }
            }
        }
    }
    public void LoseLife (int playerLifes)
    {
        if (playerLifes > 0)
            playerLifes--;
        if (playerLifes == 0)
            PlayerOut();
    }
    
    public void PlayerOut ()
    {
        posB = finalPos;
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
