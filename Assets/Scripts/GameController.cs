using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Vector3 finalPos;

    [SerializeField]
    Image endGameImage;

    [SerializeField]
    Text finalText;

    public GameObject [] players;
    public List<GameObject> playersList;
    Vector3 posB;

	void Start ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            playersList.Add(players[i]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
       
        for(int i = 0; i < players.Length - 1; i++)
        {
            if (players[i].GetComponent<PlayerMovement>() != null)
            {
                if (players[i].GetComponent<PlayerMovement>().GetLives()<=0)
                {
                    playersList.RemoveAt(i);
                }
            }
        }
        if (playersList.Count == 1)
        {
            endGameImage.gameObject.SetActive(true);
            finalText.text = playersList[0].GetComponent<Players>().inputNumber.ToString();
            Time.timeScale = 0;
        }
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
