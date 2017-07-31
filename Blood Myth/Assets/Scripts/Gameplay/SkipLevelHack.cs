using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevelHack : MonoBehaviour
    {
    public string clipName;

    public void SkipHack()
        {
        AudioManager.Instance.StopSound(clipName, AudioType.Music);
        GameManager.Instance.GetComponent<BM_SceneManager>().LoadNextScene();
        }
    }