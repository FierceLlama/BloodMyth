using UnityEngine;
using System.Collections;

/// <summary>
/// TODO (OMAR) Research Logging systems ---> will help debugging in this and next projects.
/// TODO (OMAR) Make sure this manager doesn't become a God Object.. Define specific behaviour and never increase its role.
/// TODO (OMAR) Keep Comments Updated.
/// TODO (OMAR) Rework Folder structure (remove systems folder) when done.
/// TODO (OMAR) Figure out Script Execution Order for these Systems.
/// </summary>
[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(BM_SceneManager))]
[RequireComponent(typeof(GamePlayInterface))]
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
                if (!scMan)
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
    }

    public void ChangeGameState(GameStateId GameStateID)
    {
        GameSManager.CurrentState = GameStateID;
    }

    public void LoadGameScene(SceneId inSceneID)
    {
        SceneManager.pubLoadGameScene(inSceneID);
    }
}