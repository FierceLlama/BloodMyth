using UnityEngine;
using System.Collections;

public enum ClimbingDirection
{
    CLIMBING_UP,
    CLIMBING_DOWN,
    CLIMBING_RIGHT,
    CLIMBING_LEFT,
    NOT_CLIMBING
}

public class PlayerManager : MonoBehaviour
{
    private PlayerStates _currentPlayerState;
    private PlayerStates _normalMovement;
    private PlayerStates _tiredMovement;
    private PlayerStates _exhaustedMovement;
    private PlayerStates _sprinting;
    private PlayerStates _jumping;
    private PlayerStates _climbingUp;
    private PlayerStates _climbingDown;
    private PlayerStates _climbingRight;
    private PlayerStates _climbingLeft;
    private PlayerStates _fatigueCheck;

    private PlayerMovement _playerMovementScript;
    private Player _player;

    private ClimbingDirection _climbDirection;

    void Awake()
    {
        this.Initialize();
    }

    void Initialize()
    {
        this._player = GetComponent<Player>();
        this._playerMovementScript = GetComponent<PlayerMovement>();

        this._normalMovement = new PlayerNormal(this, this._player, this._playerMovementScript);
        this._tiredMovement = new PlayerTiredMovement(this, this._player, this._playerMovementScript);
        this._exhaustedMovement = new PlayerExhaustedMovement(this, this._player, this._playerMovementScript);
        this._sprinting = new PlayerSprinting(this, this._player, this._playerMovementScript);
        this._jumping = new PlayerJumping(this, this._player, this._playerMovementScript);
        this._climbingUp = new PlayerClimbingUp(this, this._player, this._playerMovementScript);
        this._climbingDown = new PlayerClimbingDown(this, this._player, this._playerMovementScript);
        this._climbingLeft = new PlayerClimbingRight(this, this._player, this._playerMovementScript);
        this._climbingRight = new PlayerClimbingLeft(this, this._player, this._playerMovementScript);
        this._fatigueCheck = new FatigueCheck(this, this._player, this._playerMovementScript);

        this._currentPlayerState = this._normalMovement;
        this._currentPlayerState.Enter();

        this._climbDirection = ClimbingDirection.NOT_CLIMBING;
    }

    void FixedUpdate()
    {
        this._playerMovementScript.CheckOnGround();
        //*
#if UNITY_ANDROID
        if ((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Bottom || this._player.getSecondaryTouch().CurrentScreenSection == ScreenSection.Bottom)
            && !this._playerMovementScript.fatigueForJumping()
            && this._playerMovementScript.GetGrounded())
        {
            this.PlayerIsJumping();
        }

        if (((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Top && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
            || (this._player.getSecondaryTouch().CurrentScreenSection == ScreenSection.Top && this._player.getSecondaryTouch().getTouchPhase() == TouchPhase.Stationary))
            && !this._playerMovementScript.fatigueForClimbing()
            && this._playerMovementScript.canClimb())
        {
            this.DeterminePlayerClimbDirection();
        }
#endif//*/
            if (Input.GetKeyDown(KeyCode.Space)
            && !this._playerMovementScript.fatigueForJumping()
            && this._playerMovementScript.GetGrounded())
        {
            this.PlayerIsJumping();
        }
        
        if ((Input.GetKey(KeyCode.W))
            && !this._playerMovementScript.fatigueForClimbing()
            && this._playerMovementScript.canClimb())
        {
            this.DeterminePlayerClimbDirection();
        }

        this._currentPlayerState.Update();
    }

    public void SwitchPlayerState(PlayerStates newPlayerState)
    {
        this._currentPlayerState.Exit();
        this._currentPlayerState = newPlayerState;
        this._currentPlayerState.Enter();
    }

    public void PlayerIsNormal()
    {
        this.SwitchPlayerState(this._normalMovement);
    }

    public void PlayerIsTired()
    {
        this.SwitchPlayerState(this._tiredMovement);
    }

    public void PlayerIsExhausted()
    {
        this.SwitchPlayerState(this._exhaustedMovement);
    }

    public void PlayerIsSprinting()
    {
        this.SwitchPlayerState(this._sprinting);
    }

    public void PlayerIsJumping()
    {
        this.SwitchPlayerState(this._jumping);
    }

    public void DeterminePlayerClimbDirection()
    {
        switch (this._climbDirection)
        {
            case ClimbingDirection.CLIMBING_UP:
                this.SwitchPlayerState(this._climbingUp);
                break;
            case ClimbingDirection.CLIMBING_DOWN:
                this.SwitchPlayerState(this._climbingDown);
                break;
            case ClimbingDirection.CLIMBING_RIGHT:
                this.SwitchPlayerState(this._climbingRight);
                break;
            case ClimbingDirection.CLIMBING_LEFT:
                this.SwitchPlayerState(this._climbingLeft);
                break;
            default:
                break;
        }
    }

    public void CheckPlayerFatigue()
    {
        this.SwitchPlayerState(this._fatigueCheck);
    }

    public bool GetMovement()
    {
        return this._playerMovementScript.GetMovement();
    }

    public void setClimbingDirection(ClimbingDirection inClimbDirection)
    {
        this._climbDirection = inClimbDirection;
    }
}