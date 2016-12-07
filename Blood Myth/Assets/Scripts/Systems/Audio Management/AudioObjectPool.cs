using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public GameObject PlaySound(string clipname, GameObject AudioObjectParent)
    {
        GameObject GObj = FindInSFX(clipname);

        if (!GObj)
        {
            GObj = getFreeObject(AudioObjectParent);
            GObj.GetComponent<AudioObject>().ClipName = clipname;
        }

        return GObj;

    }

    private GameObject FindInSFX(string clipname)
    {
        AudioObject AObj = SFXList.Find(x => (x.ClipName == clipname) && (x.free = true));
        if (AObj) return AObj.gameObject;

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
}
