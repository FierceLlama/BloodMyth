using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    public string clipName;

    void Start()
    {
        AudioManager.Instance.PlaySound(clipName, AudioType.Music);
    }
}