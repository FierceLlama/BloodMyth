using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {

    public string DialogueKey;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DialogueManager.Instance.LoadDialogueKeys();
        DialogueManager.Instance.StartDialogue(DialogueKey);	
    }
}
