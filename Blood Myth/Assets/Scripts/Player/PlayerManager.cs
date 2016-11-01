using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    private PlayerStates _currentPlayerState;
    private PlayerStates _normalMovement;
    private PlayerStates _tiredMovement;
    private PlayerStates _exhaustedMovement;
    private PlayerStates _sprinting;
    private PlayerStates _jumping;
    private PlayerStates _climbingVertical;
    private PlayerStates _fatigueCheck;

    private PlayerMovement _playerMovementScript;
    private Player _player;

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
        this._climbingVertical = new PlayerClimbingVertical(this, this._player, this._playerMovementScript);
        this._fatigueCheck = new FatigueCheck(this, this._player, this._playerMovementScript);

        this._currentPlayerState = this._normalMovement;
        this._currentPlayerState.Enter();
    }

    void FixedUpdate()
    {
        this._playerMovementScript.CheckOnGround();

        if (Input.GetKeyUp(KeyCode.Space) && this._playerMovementScript.GetGrounded())
        {
            this.PlayerIsJumping();
        }
        /*else */
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && this._playerMovementScript.canClimb())
        {
            this.PlayerIsClimbingVertically();
        }
        this._currentPlayerState.Update();
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space) && this._playerMovementScript.GetGrounded())
    //    {
    //        this.PlayerIsJumping();
    //    }
    //}

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

    public void PlayerIsClimbingVertically()
    {
        // Need to separate climbing vertically from horizontally
        this.SwitchPlayerState(this._climbingVertical);
    }

    public void CheckPlayerFatigue()
    {
        this.SwitchPlayerState(this._fatigueCheck);
    }

    public bool GetMovement()
    {
        return this._playerMovementScript.GetMovement();
    }
}