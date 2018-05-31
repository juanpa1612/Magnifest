using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

[RequireComponent(typeof(PlayerMovementOnline))]
[RequireComponent(typeof(ChargingOnline))]
[RequireComponent(typeof(DeathScriptOnline))]
public class PlayersOnline : Photon.PunBehaviour
{
    PlayerMovementOnline playerMove;
    ChargingOnline chargingUI;
    DeathScriptOnline deathScript;
    GameControllerOnline gcOnline;
    [SerializeField] ScriptablePlayer player;

    PlayerIndex index1, index2, index3, index4;
    GamePadState prevState1, prevState2, prevState3, prevState4;
    GamePadState state1, state2, state3, state4;
    float vibrationTime;
    bool chargingTest;

    public enum InputNumber
    {
        Player1,
        Player2,
        Player3,
        Player4
    }
    public InputNumber inputNumber;

    public ScriptablePlayer Player
    {
        get
        {
            return player;
        }
    }

    void Start ()
    {
        playerMove = GetComponent<PlayerMovementOnline>();
        chargingUI = GetComponent<ChargingOnline>();
        deathScript = GetComponent<DeathScriptOnline>();
        gcOnline = GameObject.Find("GameController").GetComponent<GameControllerOnline>();
        GameObject.Instantiate(player.actualSkin,transform.position,transform.rotation,gameObject.transform);
        index1 = PlayerIndex.One;
        index2 = PlayerIndex.Two;
        index3 = PlayerIndex.Three;
        index4 = PlayerIndex.Four;
        //PlayerMovement.onHit += ControllerVibration;
	}
	
	void Update ()
    {

        chargingTest = chargingUI.isCharging;
        if (photonView.isMine)
        {
            if (state1.ThumbSticks.Left.X > 0.8f)
            {
                playerMove.ChangeDirection(true);
            }
            if (state1.ThumbSticks.Left.X < -0.8f)
            {
                playerMove.ChangeDirection(false);
            }
            if (prevState1.Buttons.RightShoulder == ButtonState.Released && state1.Buttons.RightShoulder == ButtonState.Pressed)
            {
                playerMove.ChangeRing(true);
            }
            if (prevState1.Buttons.LeftShoulder == ButtonState.Released && state1.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                playerMove.ChangeRing(false);
            }
            //Charging UI
            if (state1.Triggers.Right > 0)
            {
                chargingUI.StartCharging();
            }
            else if (state1.Triggers.Right < 0.1f)
            {
                chargingUI.StopCharging();
            }
            if (state1.Triggers.Right < 0.1f)
            {
                chargingUI.Fire();
            }
            //Scene Reset
            if (prevState1.Buttons.Back == ButtonState.Released && state1.Buttons.Back == ButtonState.Pressed)
            {
                gcOnline.GetComponent<PhotonView>().RPC("ResetGame", PhotonTargets.All);
            }
            chargingUI.ArrowDirection(state1.ThumbSticks.Left.X, state1.ThumbSticks.Left.Y);
            prevState1 = state1;
            state1 = GamePad.GetState(index1);
        }
    }
    
}
