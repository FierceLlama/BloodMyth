using UnityEngine;
using System.Collections;

/// <summary>
/// TODO (OMAR) Research Logging systems ---> will help debugging in this and next projects.
/// TODO (OMAR) Keep Comments Updated.
/// TODO (OMAR) Rework Folder structure (remove systems folder) when done.
/// </summary>
[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(BM_SceneManager))]
[RequireComponent(typeof(GamePlayInterface))]
[RequireComponent(typeof(AudioManager))]
//[RequireComponent(typeof(DialogueManager))]
//[RequireComponent(typeof(InputManager))]

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
 
    //Access to The SceneManager is Only permited through the GameManager.
    BM_SceneManager scMan;
    BM_SceneManager SceneManager
    {
        get
        {
            if (!scMan)
            {
                SceneManager = gameObject.GetComponent<BM_SceneManager>();
                if (!scMan)
                    SceneManager = gameObject.AddComponent<BM_SceneManager>();
            }

            return scMan;
        }
        set {
            if (!scMan)
                scMan = value;
        }
    }

    //Access to The GameStateManager is Only permited through the GameManager.
    GameStateManager gmMan;
    GameStateManager GameSManager
    {
        get
        {
            if (!gmMan)
            {
                GameSManager = gameObject.GetComponent<GameStateManager>();
                if (!gmMan)
                    GameSManager = gameObject.AddComponent<GameStateManager>();
            }

            return gmMan;
        }
        set
        {
            if (!gmMan)
                gmMan = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            InitGameManager();
        }
        else if (Instance != this)
            DestroyImmediate(this);
	}

    void InitGameManager()
    {
        SceneManager = gameObject.GetComponent<BM_SceneManager>();
        GameSManager = gameObject.GetComponent<GameStateManager>();
        DontDestroyOnLoad(this.gameObject);
        IOSystem.Instance.Load();
        //IOSystem.Instance.ClearData();
        SceneManager.DetermineLevelToLoad();
    }

    public void ChangeGameState(GameStateId GameStateID)
    {
        GameSManager.CurrentState = GameStateID;
    }
}