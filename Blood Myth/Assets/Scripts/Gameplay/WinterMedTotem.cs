using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterMedTotem : MonoBehaviour
    {
    private string _dialogueKey;
    public Sprite totemSprite;

    private static int _winterCount = 0;
    public int preMadeDialogues;

    void Start()
        {
        this._dialogueKey = "WINTER|MED|00" + _winterCount.ToString();

        DialogueManager.Instance.StartDialogue(_dialogueKey, totemSprite);
        _winterCount = (_winterCount + 1) % (5 + this.preMadeDialogues);
        if (_winterCount == 0)
            {
            _winterCount += this.preMadeDialogues;
            }
        this._dialogueKey = "WINTER|MED|00" + _winterCount.ToString();
        }
    }