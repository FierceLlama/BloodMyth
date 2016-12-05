using UnityEngine;
using System.Collections;

/// <summary>

/// Could be redundant with the GameStateManager
/// Maybe use this for logic related to GameStates and the Manager for switching?
/// </summary>
public class StateEngine
{
    //This can be used later if required to force or limit certain transitions
    //Basically Make sure that one state cannot go to another or that one state needs to
    //move to another when a specific thing happens.
}

public abstract class GameState
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public class MainMenuState : GameState  
{
    public MainMenuState() { }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
}

public class LoadingState : GameState
{
    public LoadingState() { }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
}

public class GameplayState : GameState
{
    public GameplayState() { }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
}