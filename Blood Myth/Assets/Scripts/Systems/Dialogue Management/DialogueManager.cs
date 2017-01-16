using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/*  
 *  Dialogue System Rules:
 *  ----------------------
 *  Add |ENM| Before a Line to tell the parser who the speaker is. 
 *  Where ENM is the Actors Enum Associated with the Speaker of that Line
 *  
 *  Add #PLAYER# Whenever the name of the player should appear. 
 *  
 *  Add Portrait to Dialogue Manager and Select Enum.
 *  
 *  Call LoadDialogueKeys() During the Level Load to load the Handles. (this Clears the previous loaded handles).
 *  Call StartDialogue( KEY ) to start the dialogue itself.
 *  
 */

[System.Serializable]
public class DialogueActor
{
    [SerializeField]
    public Actors Actor;
    [SerializeField]
    public Sprite Portrait;
}
public enum Actors
{
    PLY,
    NPC,
    TTM,
}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    DialogueLibrary dialogueLibrary;
    DialogueData CurrentDialogue;

    [SerializeField]
    public DialogueActor[] Characters;

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

    public void LoadDialogueKeys(string inKey)
    {
        dialogueLibrary.PreLoadDialogueHandle(inKey);
    } 

    public bool StartDialogue(string DialogueKey)
    {
        CurrentDialogue = dialogueLibrary.FetchDialogueData(DialogueKey);

        if (CurrentDialogue != null)
        {

            //Testing!! 
            /*
            Debug.Log(CurrentDialogue.Lines[0].line);
            Debug.Log(CurrentDialogue.Lines[0].Actor);

            Debug.Log(CurrentDialogue.Lines[1].line);
            Debug.Log(CurrentDialogue.Lines[1].Actor);

            Debug.Log(CurrentDialogue.Lines[2].line);
            Debug.Log(CurrentDialogue.Lines[2].Actor);
            */
            GameManager.Instance.ChangeGameState(GameStateId.Dialogue);

            return true;
        }
   
        return false;
    }

    public bool GetNextLine (out DialogueLine outDialogueLine)
    {
        outDialogueLine = CurrentDialogue.GetNextDialogueLine();
        
        return outDialogueLine == null ? false : true;
    }
}
