using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract class AudioObject : MonoBehaviour
{
    public bool free = false;
    private string clipname;
    public string ClipName
    {
        get { return this.clipname; }
        set
        {
            this.clipname = value;
            if (!AudioSrc)
                InitAudioSrc();

            this.AudioSrc.clip = Resources.Load(clipname) as AudioClip;
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

    public void setVolume(float newVol)
    {
        AudioSrc.volume = newVol;
    }

    public void Stop()
    {
        this.AudioSrc.Stop();
        this.free = true; // this shouldn't matter they get set to true in the Update anyway. 
                           //but good to have just in case of multiple operations in same frame.
    }
}

public class AudioObjectPool
{
    List<AudioObject> MusicList; //Music//
    List<AudioObject> SFXList;

    public void Init ()
    {
        MusicList = new List<AudioObject>();
        SFXList = new List<AudioObject>();
    }

    public void Update ()
    {
        MusicList.Sort((c1, c2) => c2.free.CompareTo(c1.free));
        SFXList.Sort((c1, c2) => c2.free.CompareTo(c1.free));
    }

    public GameObject PlaySound(string clipname, GameObject AudioObjectParent, AudioType inType)
    {
        GameObject GObj = null;
        if (inType == AudioType.SFX)
            GObj = FindInSFX(clipname);
        else if (inType == AudioType.Music)
            GObj = FindInMusic(clipname);

        if (!GObj)
        {
            GObj = getFreeObject(AudioObjectParent);
            GObj.GetComponent<AudioObject>().ClipName = clipname;
        }

        return GObj;

    }

    public GameObject StopSound (string clipname, AudioType inType)
    {
        GameObject GObj = null;
        if (inType == AudioType.SFX)
            GObj = FindInSFX(clipname);
        else if (inType == AudioType.Music)
            GObj = FindInMusic(clipname);
       
        if (GObj)
            GObj.GetComponent<AudioObject>().Stop();

        return GObj;
    }

    private GameObject FindInSFX(string clipname)
    {
        AudioObject AObj = SFXList.Find(x => (x.ClipName == clipname) && (x.free == true));

        if (AObj)
            return AObj.gameObject;

        return null;
    }

    public GameObject FindInMusic(string clipName)
        {

        AudioObject AObj = MusicList.Find(x => (x.ClipName == clipName) );

        if (AObj)
            return AObj.gameObject;

        return null;
        }
    
    public GameObject getFreeObject(GameObject AudioObjectParent)
    {
        if (AudioObjectParent.name == "SFXAudio")
        {
            if (SFXList.Count > 0)
            {
                if (SFXList[0].free == true)
                    return SFXList[0].gameObject;
            }

        }
        else if (AudioObjectParent.name == "MusicAudio")
        {
            if (MusicList.Count > 0)
            {
                if (MusicList[0].free == true)
                    return MusicList[0].gameObject;
            }
        }

        return GenerateAudioObject(AudioObjectParent);
    }

    private GameObject GenerateAudioObject(GameObject AudioObjectParent)
    {
        GameObject gObj = null;

        if (AudioObjectParent.name == "SFXAudio")
        {
            gObj = GameObject.Instantiate(Resources.Load("SFXAudioOBjectPrefab"), AudioObjectParent.transform) as GameObject;
            SFXList.Add(gObj.GetComponent<AudioObject>());

            return gObj;
        }
        else if (AudioObjectParent.name == "MusicAudio")
        {
            gObj = GameObject.Instantiate(Resources.Load("MusicAudioObjectPrefab"), AudioObjectParent.transform) as GameObject;
            MusicList.Add(gObj.GetComponent<AudioObject>());
            return gObj;
        }
      
        return gObj;
    }

    public void ApplyVolumeChange (AudioType inAudioType, float inVol)
    {
        if (inAudioType == AudioType.Music)
            foreach (AudioObject AObj in MusicList)
                AObj.setVolume(inVol);

        if (inAudioType == AudioType.SFX)
            foreach (AudioObject AObj in SFXList)
                AObj.setVolume(inVol);
    }
}
