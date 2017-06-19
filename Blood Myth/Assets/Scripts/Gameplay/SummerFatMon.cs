using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerFatMon : MonoBehaviour
    {
    public GameObject start;
    public bool completed;
    public GameObject player;
    public string dialogue;
    public GameObject fatigueMonster;

    private void Start()
        {
        this.completed = false;
        this.start.transform.position = this.player.transform.position;
        }

    public void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if(inPlayer.gameObject.tag == "Player")
            {
            if (!this.completed)
                {
                this.completed = true;
                this.player.transform.position = this.start.transform.position;
                this.player.GetComponent<Player>().addLeaf();
                DialogueManager.Instance.LoadDialogueKeys(dialogue);
                DialogueManager.Instance.StartDialogue(dialogue);
                }
            else
                {
                this.player.GetComponent<Player>().FatigueHazard();
                this.fatigueMonster.SetActive(false);
                this.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    public void KillDialogue()
        {
        DialogueManager.Instance.KillDiaglogue();
        }
    }
