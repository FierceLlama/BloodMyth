using UnityEngine;
using System.Collections;

public class AudioTesting : MonoBehaviour {

    public AudioClip X;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Play whatever by giving it the name of the clip and the type the Audio Manager takes care of everything!
            AudioManager.Instance.PlaySound("YEAH1", AudioType.SFX);
        }
    }
}
