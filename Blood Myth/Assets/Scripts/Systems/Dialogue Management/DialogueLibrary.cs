using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueHandle
{
    public string dialogueKey;
    public string directory;

    public DialogueHandle()
    {
        directory = "dialogue/";
    }
}
public class DialogueLine
{
    public Actors Actor;
    public string line;

    public DialogueLine (string inline, Actors inActor)
    {
        line = inline;
        Actor = inActor;
    }
}
public class DialogueData
{
    public int index = 0;
    public DialogueHandle handle;
    public List<DialogueLine> Lines = new List<DialogueLine>();

    public DialogueData()
    {
        Lines = new List<DialogueLine>();
    }
    public void AddLine (string inLine, Actors inActor)
    {
        Lines.Add(new DialogueLine( inLine, inActor ));
    }
    public void AddDialogueLine ( DialogueLine InDialogueLine)
    {
        Lines.Add(InDialogueLine);
    }

    public DialogueLine GetNextDialogueLine ()
    {
        if (index < Lines.Count)
            return Lines[index++];
        else
            return null;
    }
}

public class DialogueLibrary
{
    List<DialogueHandle> CurrentActiveDialogues;
    
    public DialogueLibrary()
    {
        CurrentActiveDialogues = new List<DialogueHandle>();
    }

    public DialogueData FetchDialogueData(string inKey)
    {
        DialogueData Data = new DialogueData();

        DialogueHandle Handle = this.FindDialogueHandle(inKey);
        this.ParseDialogueFile(Data, Handle);

        return Data;
    }

    public void ClearDialogueKeys()
        {
        CurrentActiveDialogues.Clear();
        }

    public void PreLoadDialogueHandle(string inKey)
    {
        //CurrentActiveDialogues.Clear();
        CurrentActiveDialogues.Add(ParseDialogueKey(inKey));
    }

    private DialogueHandle ParseDialogueKey(string inKey)
    {
        //FALL TOTEM 1    // FALL|TOTEM|001
        DialogueHandle handle = new DialogueHandle();
        handle.dialogueKey = inKey;
        
        handle.directory += inKey.Replace("|", "/").ToLower();
       
        return handle;
    }

    private DialogueLine ParseDialogueLine(string inline, Actors inActor)
    {
        string Diagline;

        if (inline[0] == '|')
        {
            inActor = (Actors)Enum.Parse(typeof(Actors), inline.Substring(1, 3));
            Diagline = inline.Remove(0, 5);
        }
        else
            Diagline = inline;

        return new DialogueLine(Regex.Replace(Diagline, "#PLAYER#", IOSystem.Instance.data.PlayerName), inActor);
    }

    private void ParseDialogueFile(DialogueData inData, DialogueHandle inHandle)
    {
        ///Use Unity's Text Assets.
        inData.handle = inHandle;
        Actors LineSpeaker = 0;
        TextAsset x = Resources.Load(inHandle.directory) as TextAsset;
 
        using (StringReader reader = new StringReader(x.text))
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                inData.AddDialogueLine(ParseDialogueLine(line, LineSpeaker));
                line = reader.ReadLine();
            } 
        }
    }
    private DialogueHandle FindDialogueHandle(string Key)
    {
        return CurrentActiveDialogues.Find(x => (x.dialogueKey == Key));
    }
}