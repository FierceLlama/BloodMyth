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
    [SerializeField]
    private float BackGroundMusicVolume;

    [Range(0, 1)]
    [SerializeField]
    private float SoundEffectsVolume;

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
        this.BackGroundMusicVolume = 1.0f;
        this.SoundEffectsVolume = 1.0f;
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
            GObj = AudioPool.PlaySound("sfx/"+clipname, SFXParentObject, inType);
            GObj.GetComponent<AudioObject>().setVolume(SoundEffectsVolume);
        }
        else if (inType == AudioType.Music)
        {
            GObj = AudioPool.PlaySound("music/"+clipname, MusicParentObject,inType);
            GObj.GetComponent<AudioObject>().SetLoopingMode(true);
            GObj.GetComponent<AudioObject>().setVolume(BackGroundMusicVolume);
        }

        GObj.GetComponent<AudioObject>().free = false;
        GObj.GetComponent<AudioObject>().PlayAudio();
    }

    public void StopSound(string clipname, AudioType inType)
    {
        GameObject GObj = null;
      
        if (inType == AudioType.SFX)
        {
            GObj = AudioPool.StopSound("sfx/" + clipname, inType);
        }
        else if (inType == AudioType.Music)
        {
            GObj = AudioPool.StopSound("music/" + clipname, inType);
        }
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
