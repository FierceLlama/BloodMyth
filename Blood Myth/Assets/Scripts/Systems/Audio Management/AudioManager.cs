using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioType
{
    SFX,
    Music
}

abstract class AudioObject : MonoBehaviour
{
    public bool free = false;
    private string clipname;
    public string ClipName
    {
        get { return this.clipname;  } 
        set
        {
            this.clipname = value;
            if (!AudioSrc)
                InitAudioSrc();

            this.AudioSrc.clip = Resources.Load(clipname) as AudioClip;
            // this.AudioSrc.clip = AudioClip.Create("MySinusoid", samplerate * 2, 1, samplerate, false, OnAudioRead, OnAudioSetPosition);
        }
    }
    public AudioSource AudioSrc;
    
    public abstract void PlayAudio();
    public abstract void UpdateVolume(float inVol);

    protected void InitAudioSrc()
    {
        AudioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void SetLoopingMode(bool Looping)
    {
        AudioSrc.loop = Looping;
    }
    protected void CheckObjectStatus()
    {
        if (!AudioSrc.isPlaying)
            free = true;
    }
}

class AudioManager: MonoBehaviour
{
    static public AudioManager Instance;

    [SerializeField]
    GameObject SFXParentObject;
    [SerializeField]
    GameObject MusicParentObject;

    [Range(0,1)]
    public float BackGroundMusicVolume;
    [Range(0, 1)]
    public float SoundEffectsVolume;

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
            GObj = AudioPool.PlaySound(clipname, SFXParentObject);
        else if (inType == AudioType.Music)
        {
            GObj = AudioPool.PlaySound(clipname, MusicParentObject);
            GObj.GetComponent<AudioObject>().SetLoopingMode(true);
        }

        GObj.GetComponent<AudioObject>().free = false;
        GObj.GetComponent<AudioObject>().PlayAudio();
    }

    public void StartLoopingSound(bool persistenc = false)
    {

    }
}
