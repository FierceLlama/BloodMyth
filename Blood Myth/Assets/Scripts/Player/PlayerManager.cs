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
    private PlayerStates _fatigueCheck;

    private Movement _playerMovementScript;
    private Player _player;

    void Awake()
    {
        this.Initialize();
    }

    void Initialize()
    {
        this._normalMovement = new PlayerNormal();
        this._tiredMovement = new PlayerTiredMovement();
        this._exhaustedMovement = new PlayerExhaustedMovement();
        this._sprinting = new PlayerSprinting();
        this._jumping = new PlayerJumping();
        this._fatigueCheck = new FatigueCheck();
        this.PlayerIsNormal();

        this._player = GetComponent<Player>();
        this._playerMovementScript = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        this._playerMovementScript.CheckOnGround();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.PlayerIsJumping();
        }
        this._currentPlayerState.Update(this, this._player, this._playerMovementScript);
    }

    public void SwitchPlayerState(PlayerStates newPlayerState)
    {
        this._currentPlayerState.Exit(this, this._player, this._playerMovementScript);
        this._currentPlayerState = newPlayerState;
        this._currentPlayerState.Enter(this, this._player, this._playerMovementScript);
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

    public void CheckPlayerFatigue()
    {
        this.SwitchPlayerState(this._fatigueCheck);
    }
}