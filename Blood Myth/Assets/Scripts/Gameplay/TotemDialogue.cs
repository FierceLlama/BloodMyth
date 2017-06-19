using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemDialogue : MonoBehaviour
    {
    public string DialogueKey;

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            DialogueManager.Instance.LoadDialogueKeys(DialogueKey);
            DialogueManager.Instance.StartDialogue(DialogueKey);
            }
        }
    }