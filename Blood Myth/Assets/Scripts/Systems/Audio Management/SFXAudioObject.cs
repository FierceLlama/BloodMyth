using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
class SFXAudioObject : AudioObject
{
    public override void PlayAudio()
    {
        if (AudioSrc == null)
            this.InitAudioSrc();
        AudioSrc.Play();
    }
    public override void UpdateVolume(float inVol)
    {
        AudioSrc.volume = inVol;
    }

    public void Update()
    {
        this.CheckObjectStatus();
    }
}