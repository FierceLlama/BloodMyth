using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemDialogue : MonoBehaviour
    {
    public string DialogueKey;
    public GameObject _player;
    private Player _playerScript;
    private SetActionIcon _action;

    void Start()
        {
        this._playerScript = this._player.GetComponent<Player>();
        this._action = GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>();
        }

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._action.DisplayIcon(SetActionIcon.IconType.INVESTIGATE);
            }
        }

    void OnTriggerStay2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            if (InputManager.instance.GetInvestigateActive())
                {
                DialogueManager.Instance.LoadDialogueKeys(DialogueKey);
                DialogueManager.Instance.StartDialogue(DialogueKey);
                }
            }
        }

    void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._action.HideIcon();
            }
        }
    }