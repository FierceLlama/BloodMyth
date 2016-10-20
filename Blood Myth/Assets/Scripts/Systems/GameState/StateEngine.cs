using UnityEngine;
using System.Collections;


/// <summary>
/// TODO (OMAR) Still not sure what to do with this may come in handy later so creatd the class
/// Could be redundant with the GameStaetManager
/// Mayabe use this for logic related to GameStates and the Manager for switching?
/// </summary>
public class StateEngine
{

}

public abstract class State
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public class MainMenuState : State
{
    public MainMenuState() { }

    public override void Enter()
    {
        Debug.Log("Idle - Enter");
    }

    public override void Exit()
    {
        Debug.Log("Idle - Exit");
    }

    public override void Update()
    {

    }
}

public class GameplayState : State
{
    public GameplayState() { }

    public override void Enter()
    {
        Debug.Log("Stunned - Enter");
    }

    public override void Exit()
    {
        Debug.Log("Stunned - Exit");
    }

    public override void Update()
    {

    }
}


