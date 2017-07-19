using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedTotemDialogue : MonoBehaviour
    {
    public string season;
    private string _dialogueKey;
    public Sprite totemSprite;

    private static int _count = 0;
    public int preMadeDialogues;

    void Start()
        {
        this._dialogueKey = season + "|MED|00" + _count.ToString();

        DialogueManager.Instance.StartDialogue(_dialogueKey, totemSprite);
        _count = (_count + 1) % (5 + this.preMadeDialogues);
        if (_count == 0)
            {
            _count += this.preMadeDialogues;
            }
        this._dialogueKey = season + "|MED|00" + _count.ToString();
        }
    }