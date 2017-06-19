using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevelHack : MonoBehaviour
    {
    public string clipName;

    public void SkipHack()
        {
        AudioManager.Instance.StopAudio(clipName);
        GameManager.Instance.GetComponent<BM_SceneManager>().LoadNextScene();
        }
    }