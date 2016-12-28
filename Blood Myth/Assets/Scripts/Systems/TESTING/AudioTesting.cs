using UnityEngine;
using System.Collections;

public class AudioTesting : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            // Play whatever by giving it the name of the clip and the type the Audio Manager takes care of everything!
            AudioManager.Instance.PlaySound("YEAH1", AudioType.SFX);
        
        if (Input.GetKeyUp(KeyCode.M))
            AudioManager.Instance.PlaySound("WOW2", AudioType.Music);

        if (Input.GetKeyUp(KeyCode.N))
            AudioManager.Instance.PlaySound("BLASTOFF", AudioType.SFX);
    }
}
