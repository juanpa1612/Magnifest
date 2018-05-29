using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerOnline : Photon.PunBehaviour
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
    public int activePlayersNumber;
    bool remove;
	void Start ()
    {
        players = new GameObject[4];
        playersList = new List<GameObject>(4);
        activePlayersNumber = players.Length;
    }
    private void Update()
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (players[i] == null)
            {
                players[i] = GameObject.Find("Scriptable Player " + (i + 1) + "(Clone)");
            }
        }
        if (playersList.Count<=4)
        {
            for (int i = 0; i < players.Length; i++)
            {
                playersList.Add(players[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        if (playersList != null)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                if (playersList[i].GetComponent<PlayerMovementOnline>().GetLifes() <= 0)
                {
                    playersList.RemoveAt(i);
                }
            }
        }


        if (playersList.Count == 1)
        {
            endGameImage.gameObject.SetActive(true);
            finalText.text = "Player " + PhotonNetwork.player.ID + " Wins!";
            Time.timeScale = 0;
        }
    }
    

    #region Singleton

    private static GameControllerOnline instance;
    private GameControllerOnline() { }

    public static GameControllerOnline Instance
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
