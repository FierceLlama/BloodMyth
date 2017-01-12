using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueHandle
{
    public string dialogueKey;
    public string directory;

    public DialogueHandle()
    {
        directory = "dialogue";
    }
}
public class DialogueData
{
    public DialogueHandle handle;
    public List<string> Lines = new List<string>();

    public void AddLines (string[] inLines)
    {
        Lines = new List<string>(inLines);
    }
    public void AddLine (string inLine)
    {
        Lines.Add(inLine);
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
    public void PreLoadDialogueHandle(string inKey)
    {
        CurrentActiveDialogues.Add(ParseDialogueKey(inKey));
    }

    private DialogueHandle ParseDialogueKey(string inKey)
    {
        //Temporary Format// 111222333
        //FALL TOTEM 1    // FLLTTM001
        DialogueHandle handle = new DialogueHandle();
        handle.dialogueKey = inKey;

        handle.directory += "/" + TranslateKey(inKey.Substring(0, 3));
        handle.directory += "/" + TranslateKey(inKey.Substring(3, 3));
        handle.directory += "/" + inKey.Substring(6, 3);
       
        return handle;
    }
    private string TranslateKey(string ParseString)
    {
        switch (ParseString)
        {
            case "FLL": return "fall";
            case "TTM": return "totem";
            default: return "";
        }
    }

    private void ParseDialogueFile(DialogueData inData, DialogueHandle inHandle)
    {

        ///Use Unity's Text Assets.
        inData.handle = inHandle;
        TextAsset x = Resources.Load(inHandle.directory) as TextAsset;
 
        using (StringReader reader = new StringReader(x.text))
        {
            string line = string.Empty;
            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    inData.AddLine(line);           
                }

            } while (line != null);
        }

        //File.ReadAllLines("Resources/"+inHandle.directory);
        
        //inData.AddLines (File.ReadAllLines(inHandle.directory));
    }
    private DialogueHandle FindDialogueHandle(string Key)
    {
        return CurrentActiveDialogues.Find(x => (x.dialogueKey == Key));
    }
}
