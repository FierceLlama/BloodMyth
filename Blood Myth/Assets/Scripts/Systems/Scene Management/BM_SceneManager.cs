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
            case (int)SceneId.Intro:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("INTRO|TOTEM|000");
                    this.sceneToLoad = SceneId.Spring;
                    break;
                    }
            case (int)SceneId.Spring:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|000");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|001");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|002");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|003");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|004");
                    DialogueManager.Instance.LoadDialogueKeys("SPRING|MED|005");
                    this.sceneToLoad = SceneId.Summer;
                    IOSystem.Instance.data.LevelsCompleted = (int)Levels.SPRING;
                    IOSystem.Instance.AutoSave();
                    break;
                    }
            case (int)SceneId.Summer:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|TOTEM|003");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|000");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|001");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|002");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|003");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|004");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|005");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|006");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|007");
                    DialogueManager.Instance.LoadDialogueKeys("SUMMER|MED|008");
                    this.sceneToLoad = SceneId.Fall;
                    IOSystem.Instance.data.LevelsCompleted = (int)Levels.SUMMER;
                    IOSystem.Instance.AutoSave();
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
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|000");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|001");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|002");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|003");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|004");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|005");
                    DialogueManager.Instance.LoadDialogueKeys("FALL|MED|006");
                    this.sceneToLoad = SceneId.Winter;
                    IOSystem.Instance.data.LevelsCompleted = (int)Levels.FALL;
                    IOSystem.Instance.AutoSave();
                    break;
                    }
            case (int)SceneId.Winter:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|001");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|002");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|TOTEM|003");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|000");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|001");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|002");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|003");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|004");
                    DialogueManager.Instance.LoadDialogueKeys("WINTER|MED|005");
                    this.sceneToLoad = SceneId.Outro;
                    IOSystem.Instance.data.LevelsCompleted = (int)Levels.WINTER;
                    IOSystem.Instance.AutoSave();
                    break;
                    }
            case (int)SceneId.Outro:
                    {
                    gpInterface.GameOnLoad(mode);
                    DialogueManager.Instance.ClearDialogueKeys();
                    DialogueManager.Instance.LoadDialogueKeys("OUTRO|TOTEM|000");
                    this.sceneToLoad = SceneId.MainMenu;
                    IOSystem.Instance.data.LevelsCompleted = (int)Levels.NEW;
                    IOSystem.Instance.AutoSave();
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

    public void DetermineLevelToLoad()
        {
        switch (IOSystem.Instance.data.LevelsCompleted)
            {
            case (0x000000002):
                    {
                    sceneToLoad = SceneId.Spring;
                    break;
                    }
            case (0x000000004):
                    {
                    sceneToLoad = SceneId.Summer;
                    break;
                    }
            case (0x000000008):
                    {
                    sceneToLoad = SceneId.Fall;
                    break;
                    }
            case (0x00000000c):
                    {
                    sceneToLoad = SceneId.Winter;
                    break;
                    }
            }
        }
    }