using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// TODO(OMAR) Talk to the team and figure out what other states are necessary (not exclusive to screens)
/// i.e. see soteria for what possible clusterfuck could come out of it.
/// </summary>
public enum GameState
{
    Gameplay = 0,
    MainMenu,
    SelectLevel,
    Settings,
    Loading,
    Pause
};

public class GameStateManager : MonoBehaviour {

    List<State> _stateslist;
    private State _currentState;
    public GameState CurrentState
    {
        get { return (GameState)this._stateslist.IndexOf(this._currentState); }
        set
        {
            if (value == (GameState)this._stateslist.IndexOf(this._currentState))
                return;

            if (_currentState != null)
                this._currentState.Exit();

            this._currentState = this._stateslist[(int)value];
            this._currentState.Enter();
        }
    }

    // Use this for initialization
    void Awake()
    {
        this._stateslist = new List<State>();
        this._stateslist.Add(new MainMenuState());
        this._stateslist.Add(new GameplayState());
    }
	
}
