using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioType
{
    SFX,
    Music
}

class AudioManager: MonoBehaviour
{
    static public AudioManager Instance;

    [SerializeField]
    GameObject SFXParentObject;
    [SerializeField]
    GameObject MusicParentObject;

    [Range(0,1)]
    private float BackGroundMusicVolume;
    [Range(0, 1)]
    private float SoundEffectsVolume;

    [SerializeField]
    AudioObjectPool AudioPool;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            InitAudioManager();
        }
        else if (Instance != this)
            DestroyImmediate(this);
    }

    void InitAudioManager()
    {
        AudioPool = new AudioObjectPool();
        AudioPool.Init();
    }

    void Update()
    {
        AudioPool.Update();
    }

    public void PlaySound(string clipname, AudioType inType)
    {
        GameObject GObj = null;

        if (inType == AudioType.SFX)
        { 
            GObj = AudioPool.PlaySound(clipname, SFXParentObject);
            GObj.GetComponent<AudioObject>().setVolume(SoundEffectsVolume);
        }
        else if (inType == AudioType.Music)
        {
            GObj = AudioPool.PlaySound(clipname, MusicParentObject);
            GObj.GetComponent<AudioObject>().SetLoopingMode(true);
            GObj.GetComponent<AudioObject>().setVolume(BackGroundMusicVolume);
        }

        GObj.GetComponent<AudioObject>().free = false;
        GObj.GetComponent<AudioObject>().PlayAudio();
    }

    public void ApplyVolumeChange(AudioType inAudioType, float inVol)
    {
        inVol = Mathf.Clamp(inVol, 0, 1);

        AudioPool.ApplyVolumeChange(inAudioType, inVol);

        if (inAudioType == AudioType.Music)
        { 
            IOSystem.Instance.data.MusicLevels = inVol;
            BackGroundMusicVolume = inVol;
        }
        if (inAudioType == AudioType.SFX)
        { 
            IOSystem.Instance.data.SFXLevels = inVol;
            SoundEffectsVolume = inVol;
        }
    }
}
