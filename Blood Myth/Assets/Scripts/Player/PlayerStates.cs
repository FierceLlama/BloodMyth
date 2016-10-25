using UnityEngine;
using System.Collections;
using System;

public abstract class PlayerStates
{
    public abstract void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript);
    public abstract void Update(PlayerManager playerManager, Player player, Movement playerMovementScript);
    public abstract void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript);
}

public class PlayerNormal : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.SetNormalMovementValues();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.NormalMovement();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerManager.PlayerIsSprinting();
        }
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerTiredMovement : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.SetTiredMovementValues();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerExhaustedMovement : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.SetExhaustedMovementValues();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerSprinting : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.SetSprintingMovementValues();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.SprintingMovement();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerManager.CheckPlayerFatigue();
        }
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerJumping : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        playerMovementScript.PlayerJumped();
        playerManager.CheckPlayerFatigue();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerClimbingVertical : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

public class PlayerClimbingHorizontal : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {

    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}

// A do nothing class which will be called upon exiting sprint, jump, and climb states
public class FatigueCheck : PlayerStates
{
    public override void Enter(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
        player.DetermineFatigue();
    }

    public override void Update(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }

    public override void Exit(PlayerManager playerManager, Player player, Movement playerMovementScript)
    {
    }
}