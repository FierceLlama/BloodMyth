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
        this._playerMovementScript.SetSprintingMovementValues();
    }

    public override void Update()
    {
        this._playerMovementScript.SprintingMovement();
        this._player.playerSprinting();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this._playerManager.CheckPlayerFatigue();
        }
    }

    public override void Exit()
    {
        this._playerMovementScript.StoppedSprinting();
    }
}

public class PlayerJumping : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerJumping(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
    {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
    }

    public override void Enter()
    {
        this._playerMovementScript.Jumped();
        this._player.playerJumped();
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
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingVertical(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
    {
        this._playerManager = playerManager;
        this._player = player;
        this._playerMovementScript = playerMovementScript;
    }

    public override void Enter()
    {
        this._playerMovementScript.StartedClimbing();
    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this._playerMovementScript.ClimbingVerticallyUp();
            this._player.playerClimbing();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this._playerMovementScript.ClimbingVerticallyDown();
            this._player.playerClimbing();
        }
        else if (!this._playerMovementScript.canClimb())
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
        //if (!this._playerMovementScript.canClimb())
        //{
        //    this._playerManager.CheckPlayerFatigue();
        //    this._playerMovementScript.StoppedClimbing();
        //}
    }
}

public class PlayerClimbingHorizontal : PlayerStates
{
    private PlayerManager _playerManager;
    private Player _player;
    private PlayerMovement _playerMovementScript;

    public PlayerClimbingHorizontal(PlayerManager playerManager, Player player, PlayerMovement playerMovementScript)
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