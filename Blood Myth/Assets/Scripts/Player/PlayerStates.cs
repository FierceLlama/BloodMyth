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
    public override void Enter()
    {
        PlayerManager.instance.SetNormalMovementValues();
    }

    public override void Update()
    {
        PlayerManager.instance.NormalMovement();
    }

    public override void Exit()
    {
    }
}

public class PlayerTiredMovement : PlayerStates
{
    public override void Enter()
    {
        PlayerManager.instance.SetTiredMovementValues();
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
    public override void Enter()
    {
        PlayerManager.instance.SetExhaustedMovementValues();
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
    public override void Enter()
    {
        PlayerManager.instance.SetSprintingMovementValues();
    }

    public override void Update()
    {
        PlayerManager.instance.SprintingMovement();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerManager.instance.PlayerIsNormal();
        }
    }

    public override void Exit()
    {
    }
}

public class PlayerJumping : PlayerStates
{
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

public class PlayerClimbingVertical : PlayerStates
{
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