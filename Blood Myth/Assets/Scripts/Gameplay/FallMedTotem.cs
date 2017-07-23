using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMedTotem : MonoBehaviour
    {
    private string _dialogueKey;
    public Sprite totemSprite;

    private static int _fallCount = 0;
    public int preMadeDialogues;

    void Start()
        {
        this._dialogueKey = "FALL|MED|00" + _fallCount.ToString();

        DialogueManager.Instance.StartDialogue(_dialogueKey, totemSprite);
        _fallCount = (_fallCount + 1) % (5 + this.preMadeDialogues);
        if (_fallCount == 0)
            {
            _fallCount += this.preMadeDialogues;
            }
        this._dialogueKey = "FALL|MED|00" + _fallCount.ToString();
        }
    }