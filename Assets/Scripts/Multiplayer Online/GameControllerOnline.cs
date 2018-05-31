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

    public List<GameObject> playersList;
    Vector3 posB;
    bool remove;
    int winnerId;
    public bool listIsFull;

	void Start ()
    {

    }
    private void Update()
    {
        if (playersList.Count == PhotonNetwork.playerList.Length)
        {
            listIsFull = true;
        }
        if (playersList.Count == 1 && listIsFull)
        {
            endGameImage.gameObject.SetActive(true);
            winnerId = playersList[0].GetComponent<PhotonView>().owner.ID;
            finalText.text = "Player " + winnerId + " Wins!";
            Time.timeScale = 0;
        }
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    [PunRPC]
    public void AddPlayerToList (int playerID)
    {
        playersList.Add(GameObject.Find("Scriptable Player " + playerID + "(Clone)"));
    }
    [PunRPC]
    public void RemovePlayerFromList (int playerID)
    {
        playersList.Remove(GameObject.Find("Scriptable Player " + playerID + "(Clone)"));
    }
    [PunRPC]
    public void ResetGame ()
    {
        PhotonNetwork.LoadLevel("PlayerSelectionOnline");
        Time.timeScale = 1;
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
