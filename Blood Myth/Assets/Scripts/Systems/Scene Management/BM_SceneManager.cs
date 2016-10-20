using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
/// TODO(OMAR) Set Game State at the end of scene load.
/// TODO(OMAR) Create Debug Scene
/// TODO (OMAR) Maybe use same scene for menus (i.e shift camera position) that would cut screen number down (loading, game, menu, +pause(additive) )
/// </summary>

///This has to be in the same order as the scenes that are loaded in the
///scene build settings, only doing this to avoid memorizing numbers.
public enum SceneId
{
    MainMenu = 0,
    Loading,
    Game,
}
//Had to do that because SceneManager already exists in the UnityEngine.SceneManagement Namespace.
public class BM_SceneManager : MonoBehaviour
{
    bool loading = false;
    [SerializeField]
    SceneId scenetoload;
    [SerializeField]
    SceneId currentScene;

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
            case (int)SceneId.Loading:
                GameObject.Find("LoadController").GetComponent<LoadingScene>().SceneToLoad = scenetoload;
            break;
            default: loading = false; break;
        }
        currentScene = (SceneId)scene.buildIndex;
    }
}