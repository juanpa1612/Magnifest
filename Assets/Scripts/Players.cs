using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ChargingUI))]
[RequireComponent(typeof(DeathScript))]
public class Players : MonoBehaviour
{
    PlayerMovement playerMove;
    ChargingUI chargingUI;
    DeathScript deathScript;
    public enum PlayerNumber
    {
        player1,
        Player2,
        Player3,
        Player4
    }
    public PlayerNumber playerNumber;

	void Start ()
    {
        playerMove = GetComponent<PlayerMovement>();
        chargingUI = GetComponent<ChargingUI>();
        deathScript = GetComponent<DeathScript>();
	}
	
	void Update ()
    {
        switch (playerNumber)
        {
            default:
                break;
            case PlayerNumber.player1:
                #region Inputs PLayer1
                if (Input.GetAxis("LeftJoystickHorizontal") > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (Input.GetAxis("LeftJoystickHorizontal") < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (Input.GetButtonDown("Right Bumper"))
                {
                    playerMove.ChangeRing(true);
                }
                if (Input.GetButtonDown("Left Bumper"))
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (Input.GetAxis("RightTrigger") > 0)
                {
                    chargingUI.StarCharging();
                }
                else if (Input.GetAxis("RightTrigger") < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (Input.GetAxis("RightTrigger") < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(Input.GetAxis("LeftJoystickHorizontal"), Input.GetAxis("LeftJoystickVertical"));
                #endregion
                break;

            case PlayerNumber.Player2:
                #region Inputs Player2
                if (Input.GetAxis("LeftJoystick2Horizontal") > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (Input.GetAxis("LeftJoystick2Horizontal") < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (Input.GetButtonDown("Right Bumper 2"))
                {
                    playerMove.ChangeRing(true);
                }
                if (Input.GetButtonDown("Left Bumper 2"))
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (Input.GetAxis("RightTrigger2") > 0)
                {
                    chargingUI.StarCharging();
                }
                else if (Input.GetAxis("RightTrigger2") < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (Input.GetAxis("RightTrigger2") < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(Input.GetAxis("LeftJoystick2Horizontal"), Input.GetAxis("LeftJoystick2Vertical"));
                #endregion
                break;

            case PlayerNumber.Player3:
                #region Inputs Player3
                if (Input.GetAxis("LeftJoystick3Horizontal") > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (Input.GetAxis("LeftJoystick3Horizontal") < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (Input.GetButtonDown("Right Bumper 3"))
                {
                    playerMove.ChangeRing(true);
                }
                if (Input.GetButtonDown("Left Bumper 3"))
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (Input.GetAxis("RightTrigger3") > 0)
                {
                    chargingUI.StarCharging();
                }
                else if (Input.GetAxis("RightTrigger3") < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (Input.GetAxis("RightTrigger3") < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(Input.GetAxis("LeftJoystick3Horizontal"), Input.GetAxis("LeftJoystick3Vertical"));
                #endregion
                break;

            case PlayerNumber.Player4:
                #region Inputs Player4
                if (Input.GetAxis("LeftJoystick4Horizontal") > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (Input.GetAxis("LeftJoystick4Horizontal") < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (Input.GetButtonDown("Right Bumper 4"))
                {
                    playerMove.ChangeRing(true);
                }
                if (Input.GetButtonDown("Left Bumper 4"))
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (Input.GetAxis("RightTrigger4") > 0)
                {
                    chargingUI.StarCharging();
                }
                else if (Input.GetAxis("RightTrigger4") < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (Input.GetAxis("RightTrigger4") < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(Input.GetAxis("LeftJoystick4Horizontal"), Input.GetAxis("LeftJoystick4Vertical"));
                #endregion
                break;
        }
        
    }
}
