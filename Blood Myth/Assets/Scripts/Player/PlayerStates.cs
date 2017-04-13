using UnityEngine;
using System.Collections;
using System;

public abstract class PlayerStates
    {
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    }

public class PlayerNormal : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerNormal(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }
    
    public override void Enter()
        {
        this._playerMovementScript.SetNormalMovementValues();
        }

    public override void Update()
        {
        if (this._playerMovementScript.GetMovement())
        {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Run_Normal";
        }
        else if (!this._playerMovementScript.GetMovement() && !this._playerMovementScript._isSprinting && this._playerMovementScript.GetGrounded())
        {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Idle_Normal";
        }

        this._playerMovementScript.NormalMovement();
        //* When using Android
#if UNITY_ANDROID
        //add isSprint check
        if ((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right || this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left)
            && this._player.getPrimaryTouch().getTouchTapCount() >= 2 && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
        {
            if(!this._playerMovementScript._canRun) return;
            this._playerManager.PlayerIsSprinting();
            this._playerMovementScript.tutorialManager.PassedRun();
        }
#endif//*/

#if UNITY_EDITOR
        //* When using editor
        if (Input.GetKey(KeyCode.LeftShift))
            {
            if (!this._playerMovementScript._canRun) return;
            this._playerManager.PlayerIsSprinting();
            }//*/
#endif
        }

    public override void Exit()
        {
        }
    }

public class PlayerTiredMovement : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerTiredMovement(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.SetTiredMovementValues();
        }

    public override void Update()
        {
        if (this._playerMovementScript.GetMovement())
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Run_Tired";
            }
        else if (!this._playerMovementScript.GetMovement() && !this._playerMovementScript._isSprinting && this._playerMovementScript.GetGrounded())
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Idle_Tired";
            }
        this._playerMovementScript.TiredMovement();
        }

    public override void Exit()
        {
        }
    }

public class PlayerExhaustedMovement : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerExhaustedMovement(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.SetExhaustedMovementValues();
        }

    public override void Update()
        {
        if (this._playerMovementScript.GetMovement())
            {

            this._playerMovementScript._skeletonAnimation.AnimationName = "Run_Exhausted";
            }
        else if (!this._playerMovementScript.GetMovement() && !this._playerMovementScript._isSprinting && this._playerMovementScript.GetGrounded())
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Idle_Exhausted";
            }
        this._playerMovementScript.ExhaustedMovement();
        }

    public override void Exit()
        {
        }
    }

public class PlayerSprinting : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;


    public PlayerSprinting(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript._isSprinting = true;
        this._playerMovementScript.SetSprintingMovementValues();
        }

    public override void Update()
        {
        if (this._playerMovementScript._isSprinting && !this._playerManager._isTired)
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Sprint_Normal";
            }
        else if (this._playerMovementScript._isSprinting && this._playerManager._isTired)
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Sprint_Tired";
            }
        this._playerMovementScript.SprintingMovement();
        this._player.Sprinting();

        //* When using Android
#if UNITY_ANDROID
        if ((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right || this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left)
            && this._player.getPrimaryTouch().getTouchTapCount() >= 2 && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
        {
            this._playerManager.CheckPlayerFatigue();
        }
        else
        {
            this._playerManager.PlayerIsNormal();
        }
#endif//*/

#if UNITY_EDITOR
        //* When using editor
        if (Input.GetKeyUp(KeyCode.LeftShift))
            {
            this._playerManager.CheckPlayerFatigue();
            }//*/
#endif
        }

    public override void Exit()
        {
        this._playerMovementScript._isSprinting = false;
        this._playerMovementScript.StoppedSprinting();
        }
    }

public class PlayerJumping : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;
    private bool ok;

    public PlayerJumping(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {


        }


    public override void Update()
        {
        if (this._playerManager._isTired)
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Jump_Tired";
            }
        else if (this._playerManager._isExhausted)
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Jump_Exhausted";
            }
        else
            {
            this._playerMovementScript._skeletonAnimation.AnimationName = "Jump_Normal";
            }

        AudioManager.Instance.PlaySound("Jump", AudioType.SFX);

        this._playerMovementScript.Jumped();
        this._player.Jumped();
        }

    public override void Exit()
        {

        }
    }

public class PlayerClimbingUp : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingUp(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.StartedClimbingVertically();
        }

    public override void Update()
        {
        //* When using Android
#if UNITY_ANDROID
        if (this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Top
            && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary
            && this._playerMovementScript.canClimb())
        {
            this._playerMovementScript.ClimbingUp();
            this._player.Climbing();
            this._playerMovementScript._skeletonAnimation.AnimationName = "Climb_Vertical";

        }
#endif//*/

#if UNITY_EDITOR
        //* When using editor
        if (Input.GetKey(KeyCode.W) && this._playerMovementScript.canClimb())
            {
            this._playerMovementScript.ClimbingUp();
            this._player.Climbing();
            this._playerMovementScript._skeletonAnimation.AnimationName = "Climb_Vertical";
            }//*/
#endif
        else if (!this._playerMovementScript.canClimb() || this._playerMovementScript.GetGrounded())
            {
            this._playerManager.CheckPlayerFatigue();
            this._playerMovementScript.StoppedClimbing();

            }
        else
            {
            this._playerMovementScript.StationaryWhileClimbing();
            }
        }

    public override void Exit()
        {
        }
    }

public class PlayerClimbingDown : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingDown(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.StartedClimbingVertically();
        }

    public override void Update()
        {
        //* When using Andriod
#if UNITY_ANDROID
        if (this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Top
            && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary
            && this._playerMovementScript.canClimb())
        {
            this._playerMovementScript.ClimbingDown();
            this._player.Climbing();
            this._playerMovementScript._skeletonAnimation.AnimationName = "Climb_Vertical";

        }
#endif//*/

#if UNITY_EDITOR
        //* When using editor
        if (Input.GetKey(KeyCode.W) && this._playerMovementScript.canClimb())
            {
            this._playerMovementScript.ClimbingDown();
            this._player.Climbing();
            this._playerMovementScript._skeletonAnimation.AnimationName = "Climb_Vertical";
            }//*/
#endif
        else if (!this._playerMovementScript.canClimb() || this._playerMovementScript.GetGrounded())
            {
            this._playerManager.CheckPlayerFatigue();
            this._playerMovementScript.StoppedClimbing();
            }
        else
            {
            this._playerMovementScript.StationaryWhileClimbing();
            }
        }

    public override void Exit()
        {
        }
    }

public class PlayerClimbingRight : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingRight(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.StartedClimbingHorizontally();
        }

    public override void Update()
        {
        //* When using editor
        if (Input.GetKey(KeyCode.W) && this._playerMovementScript.canClimb())
            {
            this._playerMovementScript.ClimbingRight();
            this._player.Climbing();
            }//*/

        else if (!this._playerMovementScript.canClimb() || this._playerMovementScript.GetGrounded())
            {
            this._playerManager.CheckPlayerFatigue();
            this._playerMovementScript.StoppedClimbing();
            }
        else
            {
            this._playerMovementScript.StationaryWhileClimbing();
            }
        }

    public override void Exit()
        {
        }
    }

public class PlayerClimbingLeft : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingLeft(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._playerMovementScript.StartedClimbingHorizontally();
        }

    public override void Update()
        {
        //* When using editor
        if (Input.GetKey(KeyCode.W) && this._playerMovementScript.canClimb())
            {
            this._playerMovementScript.ClimbingLeft();
            this._player.Climbing();
            }//*/

        else if (!this._playerMovementScript.canClimb() || this._playerMovementScript.GetGrounded())
            {
            this._playerManager.CheckPlayerFatigue();
            this._playerMovementScript.StoppedClimbing();
            }
        else
            {
            this._playerMovementScript.StationaryWhileClimbing();
            }
        }

    public override void Exit()
        {
        }
    }

// A do nothing class which will be called upon exiting sprint, jump, and climb states
public class FatigueCheck : PlayerStates
    {
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public FatigueCheck(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
        {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
        }

    public override void Enter()
        {
        this._player.DetermineFatigue();
        }

    public override void Update()
        {
        }

    public override void Exit()
        {
        }
    }