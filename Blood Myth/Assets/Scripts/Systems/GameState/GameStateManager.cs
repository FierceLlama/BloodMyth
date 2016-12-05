using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO(OMAR) Talk to the team and figure out what other states are necessary (not exclusive to screens)
/// i.e. see soteria for what possible clusterfuck could come out of it.
/// </summary>
public enum GameStateId
{
    // All game state related info (not scene change stuff)
    MainMenu = 0,
    Loading,
    Gameplay,
    SelectLevel,
    Settings,
    Pause
};

public class GameStateManager : MonoBehaviour
{
    ///for the inspector
    [SerializeField]
    private GameStateId currentGameState;

    List<GameState> _stateslist;
    private GameState _currentState;
    public GameStateId CurrentState
    {
        get
        {
            return (GameStateId)this._stateslist.IndexOf(this._currentState);
        }

        set
        {
            if (value == (GameStateId)this._stateslist.IndexOf(this._currentState))
            {
                return;
            }

            if (_currentState != null)
            {
                this._currentState.Exit();
            }

            this._currentState = this._stateslist[(int)value];
            this.currentGameState = value;
            this._currentState.Enter();
        }
    }

    // Use this for initialization
    void Awake()
    {
        _currentState = null;
        this._stateslist = new List<GameState>();
        this._stateslist.Add(new MainMenuState());
        this._stateslist.Add(new LoadingState());
        this._stateslist.Add(new GameplayState());
    }

    void Update ()
    {
        _currentState.Update();
    }
}