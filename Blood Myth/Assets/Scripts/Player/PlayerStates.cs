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
    private Movement _playerMovementScript;

    public PlayerNormal(PlayerManager playerManager, Player player, Movement playerMovementScript)
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
        this._playerMovementScript.NormalMovement();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this._playerManager.PlayerIsSprinting();
        }
    }

    public override void Exit()
    {
    }
}

public class PlayerTiredMovement : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerTiredMovement(PlayerManager playerManager, Player player, Movement playerMovementScript)
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
    }

    public override void Exit()
    {
    }
}

public class PlayerExhaustedMovement : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerExhaustedMovement(PlayerManager playerManager, Player player, Movement playerMovementScript)
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
    }

    public override void Exit()
    {
    }
}

public class PlayerSprinting : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerSprinting(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
    }

    public override void Enter()
    {
        this._playerMovementScript.SetSprintingMovementValues();
    }

    public override void Update()
    {
        this._playerMovementScript.SprintingMovement();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this._playerManager.CheckPlayerFatigue();
        }
    }

    public override void Exit()
    {
    }
}

public class PlayerJumping : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerJumping(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
    }

    public override void Enter()
    {
        this._playerMovementScript.PlayerJumped();
        this._playerManager.CheckPlayerFatigue();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

public class PlayerClimbingVertical : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerClimbingVertical(PlayerManager playerManager, Player player, Movement playerMovementScript)
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
    }

    public override void Exit()
    {
    }
}

public class PlayerClimbingHorizontal : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private Movement _playerMovementScript;

    public PlayerClimbingHorizontal(PlayerManager playerManager, Player player, Movement playerMovementScript)
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
    private Movement _playerMovementScript;

    public FatigueCheck(PlayerManager playerManager, Player player, Movement playerMovementScript)
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