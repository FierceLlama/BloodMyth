using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {

    public string DialogueKey;
	
	// Update is called once per frame
	void Start ()
    {
        DialogueManager.Instance.LoadDialogueKeys(DialogueKey);
        //DialogueManager.Instance.StartDialogue(DialogueKey);	
    }
}
