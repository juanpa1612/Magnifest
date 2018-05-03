﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ChargingUI))]
[RequireComponent(typeof(DeathScript))]
public class PlayersMultiplayer : Photon.PunBehaviour
{
    PlayerMovement playerMove;
    ChargingUI chargingUI;
    DeathScript deathScript;

    [SerializeField] ScriptablePlayer player;

    PlayerIndex index1, index2, index3, index4;
    GamePadState prevState1, prevState2, prevState3, prevState4;
    GamePadState state1, state2, state3, state4;
    float vibrationTime;

    public enum InputNumber
    {
        Player1,
        Player2,
        Player3,
        Player4
    }
    public InputNumber inputNumber;
    
	void Start ()
    {
        if (!photonView.isMine)
        {
            Destroy(this);
        }
        playerMove = GetComponent<PlayerMovement>();
        chargingUI = GetComponent<ChargingUI>();
        deathScript = GetComponent<DeathScript>();
        GameObject.Instantiate(player.actualSkin,transform.position,transform.rotation,gameObject.transform);
        index1 = PlayerIndex.One;
        index2 = PlayerIndex.Two;
        index3 = PlayerIndex.Three;
        index4 = PlayerIndex.Four;
        //PlayerMovement.onHit += ControllerVibration;
	}
	
	void Update ()
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
                SceneManager.LoadScene("PlayerSelection");
                Time.timeScale = 1;
            }
            chargingUI.ArrowDirection(state1.ThumbSticks.Left.X, state1.ThumbSticks.Left.Y);
            prevState1 = state1;
            state1 = GamePad.GetState(index1);
    }
    
}