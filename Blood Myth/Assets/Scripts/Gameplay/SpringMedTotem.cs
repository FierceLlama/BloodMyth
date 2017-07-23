using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMedTotem : MonoBehaviour
    {
    private string _dialogueKey;
    public Sprite totemSprite;

    private static int _springCount = 0;
    public int preMadeDialogues;

    void Start()
        {
        this._dialogueKey = "SPRING|MED|00" + _springCount.ToString();

        DialogueManager.Instance.StartDialogue(_dialogueKey, totemSprite);
        _springCount = (_springCount + 1) % (5 + this.preMadeDialogues);
        if (_springCount == 0)
            {
            _springCount += this.preMadeDialogues;
            }
        this._dialogueKey = "SPRING|MED|00" + _springCount.ToString();
        }
    }