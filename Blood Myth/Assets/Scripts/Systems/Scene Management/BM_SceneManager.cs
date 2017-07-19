using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Had to do that because SceneManager already exists in the UnityEngine.SceneManagement Namespace.
//Will refactor later.
public class BM_SceneManager : MonoBehaviour
    {
    bool loading = false;
    [SerializeField]
    SceneId sceneToLoad;
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
        sceneToLoad = inLevelID;
        loading = true;
        SceneManager.LoadScene((int)SceneId.Loading);
        }

    void LoadMenuScene(SceneId inLevelID)
        {
        sceneToLoad = inLevelID;
        loading = true;
        SceneManager.LoadScene((int)sceneToLoad);
        }

    //DEBUG
    void Update()
        {
        //if (Input.GetKeyUp(KeyCode.Space) && loading == false)
        //{
        //    LoadGameScene(SceneId.Level1);
        //}
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
        //TODO (CHRIS) 
        //ADD Dialogue Handle Loading in here so the dialogue handles are preloaded
        //At Scene Load.
        switch (scene.buildIndex)
            {
            case (int)SceneId.MainMenu:
                    {
                    gpInterface.MainMenuOnLoad(mode);
                    break;
                    }
            case (int)SceneId.Loading:
                    {
                    GameObject.Find("LoadController").GetComponent<LoadingScene>().SceneToLoad = sceneToLoad;
                    gpInterface.LoadingOnLoad(mode);
                    break;
                    }
            case (int)SceneId.Spring:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|TOTEM|002");
                    this.sceneToLoad = SceneId.Summer;
                    break;
                    }
            case (int)SceneId.Summer:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|003");
                    this.sceneToLoad = SceneId.Fall;
                    break;
                    }
            case (int)SceneId.Fall:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("FALL|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|TOTEM|003");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|TOTEM|004");
                    this.sceneToLoad = SceneId.Winter;
                    break;
                    }
            case (int)SceneId.Winter:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|003");
                    this.sceneToLoad = SceneId.MainMenu;
                    break;
                    }
            default:
                    {
                    loading = false;
                    break;
                    }
            }

        currentScene = (SceneId)scene.buildIndex;
        }

    public void pubLoadGameScene(SceneId inSceneID)
        {
        this.LoadGameScene(inSceneID);
        }

    public void LoadNextScene()
        {
        this.LoadGameScene(sceneToLoad);
        }

    public void ResetScene()
        {
        this.LoadGameScene(currentScene);
        }
    }