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
    public DialogueActor currentActor;

    [SerializeField]
    public DialogueActor[] Characters;

    public GameObject diagDisplay;
    public GameObject playerPortrait;
    public Sprite playerSprite;
    public GameObject NPCPortrait;
    private Sprite _NPCSprite;
    private UnityEngine.UI.Image _NPCImage;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            InitDialogueManager();
        }
        else if (Instance != this)
            DestroyImmediate(this);

        this.diagDisplay.SetActive(false);
        this.playerPortrait.GetComponent<UnityEngine.UI.Image>().sprite = this.playerSprite;
        this.playerPortrait.SetActive(false);
        this._NPCImage = this.NPCPortrait.GetComponent<UnityEngine.UI.Image>();
        this.NPCPortrait.SetActive(false);
    }

    void InitDialogueManager()
    {
        dialogueLibrary = new DialogueLibrary();
    }

    public void ClearDialogueKeys()
        {
        dialogueLibrary.ClearDialogueKeys();
        }

    public void LoadDialogueKeys(string inKey)
    {
        dialogueLibrary.PreLoadDialogueHandle(inKey);
    }

    public bool StartDialogue(string DialogueKey, Sprite inNPCSprite)
    {
        this.diagDisplay.SetActive(true);
        this.playerPortrait.SetActive(true);
        this.NPCPortrait.SetActive(true);
        this._NPCImage.sprite = inNPCSprite;
        CurrentDialogue = dialogueLibrary.FetchDialogueData(DialogueKey);

        if (CurrentDialogue != null)
        {
            GameManager.Instance.ChangeGameState(GameStateId.Dialogue);
            string s = "";
            for (int i = 0; i < CurrentDialogue.Lines.Count; i++)
                {
                s += CurrentDialogue.Lines[i].line;
                //s += ",";
                }
            s = s.Replace("/", "\n");
            this.diagDisplay.GetComponentInChildren<UnityEngine.UI.Text>().text = s;
            //Testing!! 

            //Debug.Log(CurrentDialogue.Lines[0].line);
            //Debug.Log(CurrentDialogue.Lines[0].Actor);

            //Debug.Log(CurrentDialogue.Lines[1].line);
            //Debug.Log(CurrentDialogue.Lines[1].Actor);

            //Debug.Log(CurrentDialogue.Lines[2].line);
            //Debug.Log(CurrentDialogue.Lines[2].Actor);

            return true;
        }
   
        return false;
    }


    public bool GetNextLine(out DialogueLine outDialogueLine)
    {
        if ( privGetNextLine(out outDialogueLine) )
        {
            currentActor = GetNextActor(outDialogueLine);
            return true;
        }

        return false;
           
    }
    public DialogueActor GetNextActor(DialogueLine inDialogue)
    {
        for (int i = 0; i < Characters.Length; ++i)
        {
            if (Characters[i].Actor == inDialogue.Actor)
                return Characters[i];
        }
        return null;
    }
    public bool privGetNextLine (out DialogueLine outDialogueLine)
    {
        outDialogueLine = CurrentDialogue.GetNextDialogueLine();
        
        return outDialogueLine == null ? false : true;
    }

    public void KillDiaglogue()
        {
        GameManager.Instance.ChangeGameState(GameStateId.Gameplay);
        this.diagDisplay.SetActive(false);
        this.playerPortrait.SetActive(false);
        this.NPCPortrait.SetActive(false);
        }
    }