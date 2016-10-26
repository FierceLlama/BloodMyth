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
        this._player = GetComponent<Player>();
        this._playerMovementScript = GetComponent<Movement>();

        this._normalMovement = new PlayerNormal(this, this._player, this._playerMovementScript);
        this._tiredMovement = new PlayerTiredMovement(this, this._player, this._playerMovementScript);
        this._exhaustedMovement = new PlayerExhaustedMovement(this, this._player, this._playerMovementScript);
        this._sprinting = new PlayerSprinting(this, this._player, this._playerMovementScript);
        this._jumping = new PlayerJumping(this, this._player, this._playerMovementScript);
        this._fatigueCheck = new FatigueCheck(this, this._player, this._playerMovementScript);
        this.PlayerIsNormal();
    }

    void FixedUpdate()
    {
        this._playerMovementScript.CheckOnGround();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.PlayerIsJumping();
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

    public void CheckPlayerFatigue()
    {
        this.SwitchPlayerState(this._fatigueCheck);
    }
}