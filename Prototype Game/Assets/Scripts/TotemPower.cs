using UnityEngine;
using System.Collections;

public class TotemPower : MonoBehaviour {

    public bool hasTotemPower = false;
    public ParticleSystem TotemHalo;

    public string curPower;
	
    void Start()
    {

    }

	void Update () 
    {
        if (hasTotemPower && !TotemHalo.isPlaying)
            TotemHalo.Play();
        else if (!hasTotemPower)
            TotemHalo.Stop();

	}


}
