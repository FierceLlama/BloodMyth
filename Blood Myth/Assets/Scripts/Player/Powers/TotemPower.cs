using UnityEngine;
using System.Collections;

public class TotemPower : MonoBehaviour
{
    public bool hasTotemPower = false;
    public ParticleSystem TotemHalo;
    public string curPower;
	
    void Start()
    {
        // Might need to figure out actual particle location in z space to determine sorting layer
        //      For now I am ensuring it is in front of player
        TotemHalo.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

	void Update () 
    {
        if (hasTotemPower && !TotemHalo.isPlaying)
            TotemHalo.Play();
        else if (!hasTotemPower)
            TotemHalo.Stop();

	}
}