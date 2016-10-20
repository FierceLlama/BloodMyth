using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>

/// </summary>

//Had to do that because SceneManager already exists in the UnityEngine.SceneManagement Namespace.
public class BM_SceneManager : MonoBehaviour
{
    bool loading = false;
    [SerializeField]
    SceneId scenetoload;
    [SerializeField]
    SceneId currentScene;
    GamePlayInterface gpI;
    GamePlayInterface gpInterface
    {
        get
        {
            if (!gpI)
            {
                gpInterface = gameObject.GetComponent<GamePlayInterface>();
                if (!gpI)
                    gpInterface = gameObject.AddComponent<GamePlayInterface>();
            }

            return gpI;
        }
        set
        {
            if (!gpI)
                gpI = value;
        }
    }

    void Awake()
    {

    }

    void LoadGameScene(SceneId inLevelID)
    {
        scenetoload = inLevelID;
        loading = true;
        SceneManager.LoadScene((int)SceneId.Loading);
    }
    void LoadMenuScene (SceneId inLevelID)
    {
        scenetoload = inLevelID;
        loading = true;
        SceneManager.LoadScene((int)scenetoload);
    }

    //DEBUG
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && loading == false)
        {
            LoadGameScene(SceneId.Game);
        }
    }

    //Using Delegates to Make something Happen when a scene is done loading.
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            case (int)SceneId.MainMenu:
                gpInterface.MainMenuOnLoad(mode);
            break;

            case (int)SceneId.Loading:
                GameObject.Find("LoadController").GetComponent<LoadingScene>().SceneToLoad = scenetoload;
                gpInterface.LoadingOnLoad(mode);
            break;

            case (int)SceneId.Game:
                gpInterface.GameOnLoad(mode);
            break;

            default: loading = false; break;
        }
        currentScene = (SceneId)scene.buildIndex;
    }
}