using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    bool Sceneloaded = false;
    SceneId privSceneToLoad;
    AsyncOperation async;

    public SceneId SceneToLoad
    {
        get { return privSceneToLoad; }
        set
        {
            privSceneToLoad = value;

            if (privSceneToLoad != SceneId.Loading && Sceneloaded == false)
            {
                StartCoroutine(CheckSceneLoadStatus());
                Sceneloaded = false;
            }
            else
            {
                Debug.Assert(true, "Something is Wrong, Loading scene is trying to Load itself.");
            }
        }
    }

    IEnumerator CheckSceneLoadStatus()
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        async = SceneManager.LoadSceneAsync((int)privSceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

        Sceneloaded = true;
        privSceneToLoad = SceneId.Loading;
    }

    public void Awake()
    {
        IOSystem.Instance.AutoSave();
    }
}