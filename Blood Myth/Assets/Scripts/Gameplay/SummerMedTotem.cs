using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerMedTotem : MonoBehaviour
    {
    private string _dialogueKey;
    public Sprite totemSprite;

    private static int _summerCount = 0;
    public int preMadeDialogues;

    void Start()
        {
        this._dialogueKey = "SUMMER|MED|00" + _summerCount.ToString();

        DialogueManager.Instance.StartDialogue(_dialogueKey, totemSprite);
        _summerCount = (_summerCount + 1) % (5 + this.preMadeDialogues);
        if (_summerCount == 0)
            {
            _summerCount += this.preMadeDialogues;
            }
        this._dialogueKey = "SUMMER|MED|00" + _summerCount.ToString();
        }
    }