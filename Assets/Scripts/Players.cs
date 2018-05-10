using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Charging))]
[RequireComponent(typeof(DeathScript))]
public class Players : MonoBehaviour
{
    PlayerMovement playerMove;
    Charging chargingUI;
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
        playerMove = GetComponent<PlayerMovement>();
        chargingUI = GetComponent<Charging>();
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
        switch (inputNumber)
        {
            default:
                break;
            case InputNumber.Player1:
                #region Inputs PLayer1
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
                #endregion
                break;

            case InputNumber.Player2:
                #region Inputs Player2
                if (state2.ThumbSticks.Left.X > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (state2.ThumbSticks.Left.X < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (prevState2.Buttons.RightShoulder == ButtonState.Released && state2.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(true);
                }
                if (prevState2.Buttons.LeftShoulder == ButtonState.Released && state2.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (state2.Triggers.Right > 0)
                {
                    chargingUI.StartCharging();
                }
                else if (state2.Triggers.Right < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (state2.Triggers.Right < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(state2.ThumbSticks.Left.X, state2.ThumbSticks.Left.Y);
                if (prevState2.Buttons.Back == ButtonState.Released && state2.Buttons.Back == ButtonState.Pressed)
                {
                    SceneManager.LoadScene("PlayerSelection");
                    Time.timeScale = 1;
                }
                #endregion
                break;

            case InputNumber.Player3:
                #region Inputs Player3
                if (state3.ThumbSticks.Left.X > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (state3.ThumbSticks.Left.X < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (prevState3.Buttons.RightShoulder == ButtonState.Released && state3.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(true);
                }
                if (prevState3.Buttons.LeftShoulder == ButtonState.Released && state3.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (state3.Triggers.Right > 0)
                {
                    chargingUI.StartCharging();
                }
                else if (state3.Triggers.Right < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (state3.Triggers.Right < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(state3.ThumbSticks.Left.X, state3.ThumbSticks.Left.Y);
                if (prevState3.Buttons.Back == ButtonState.Released && state3.Buttons.Back == ButtonState.Pressed)
                {
                    SceneManager.LoadScene("PlayerSelection");
                    Time.timeScale = 1;
                }
                #endregion
                break;

            case InputNumber.Player4:
                #region Inputs Player4
                if (state4.ThumbSticks.Left.X > 0.8f)
                {
                    playerMove.ChangeDirection(true);
                }
                if (state4.ThumbSticks.Left.X < -0.8f)
                {
                    playerMove.ChangeDirection(false);
                }
                if (prevState4.Buttons.RightShoulder == ButtonState.Released && state4.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(true);
                }
                if (prevState4.Buttons.LeftShoulder == ButtonState.Released && state4.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    playerMove.ChangeRing(false);
                }
                //Charging UI
                if (state4.Triggers.Right > 0)
                {
                    chargingUI.StartCharging();
                }
                else if (state4.Triggers.Right < 0.1f)
                {
                    chargingUI.StopCharging();
                }
                if (state4.Triggers.Right < 0.1f)
                {
                    chargingUI.Fire();
                }
                chargingUI.ArrowDirection(state4.ThumbSticks.Left.X, state4.ThumbSticks.Left.Y);
                if (prevState4.Buttons.Back == ButtonState.Released && state4.Buttons.Back == ButtonState.Pressed)
                {
                    SceneManager.LoadScene("PlayerSelection");
                    Time.timeScale = 1;
                }
                #endregion
                break;
        }
        prevState1 = state1;
        prevState2 = state2;
        prevState3 = state3;
        prevState4 = state4;
        state1 = GamePad.GetState(index1);
        state2 = GamePad.GetState(index2);
        state3 = GamePad.GetState(index3);
        state4 = GamePad.GetState(index4);
        #region Inputs Player2
        /* Version Anterior
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
            chargingUI.StartCharging();
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
        */
        #endregion
    }
    public void ControllerVibration()
    {
        switch (inputNumber)
        {
            case InputNumber.Player1:
                Debug.Log("Vibración");
                vibrationTime = 2f;
                if (vibrationTime > 0)
                {
                    GamePad.SetVibration(index1, 0, 0.5f);
                }
                break;
            case InputNumber.Player2:
                Debug.Log("Vibración2");
                vibrationTime = 2f;
                if (vibrationTime > 0)
                {
                    GamePad.SetVibration(index1, 0, 0.5f);
                }
                break;
            case InputNumber.Player3:
                break;
            case InputNumber.Player4:
                break;
            default:
                break;
        }
    }
}
