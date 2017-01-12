using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    DialogueLibrary dialogueLibrary;
    DialogueData CurrentDialogue;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            InitDialogueManager();
        }
        else if (Instance != this)
            DestroyImmediate(this);
    }

    void InitDialogueManager()
    {
        dialogueLibrary = new DialogueLibrary();
    }

    /// TEST
    public void LoadDialogueKeys()
    {
        dialogueLibrary.PreLoadDialogueHandle("FLLTTM001");
    } 

    public bool StartDialogue(string DialogueKey)
    {
        CurrentDialogue = dialogueLibrary.FetchDialogueData(DialogueKey);

        if (CurrentDialogue != null)
        {
            //TODO(OMAR)
            //Start Dialogue Interface// etc..
            //Change the Games State?
            //Make sure the Pause Function works in HERE
            return true;
        }
   
        return false;
    }
}
