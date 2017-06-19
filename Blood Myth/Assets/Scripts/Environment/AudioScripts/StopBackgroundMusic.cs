using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBackgroundMusic : MonoBehaviour
    {
    public string clipName;

    public void StopMusic()
        {
        AudioManager.Instance.StopAudio(clipName);
        }
    }