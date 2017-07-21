using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerFatMon : MonoBehaviour
    {
    private static bool doneThisShitAlready;
    public GameObject start;
    public bool completed;
    public GameObject player;

    private void Start()
        {
        if (doneThisShitAlready)
            {
            this.gameObject.SetActive(false);
            }
        else
            {
            this.completed = false;
            this.start.transform.position = this.player.transform.position;
            }
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
                }
            else
                {
                doneThisShitAlready = true;
                this.player.GetComponent<Player>().FatigueHazard();
                this.gameObject.SetActive(false);
                }
            }
        }
    public void KillDialogue()
        {
        DialogueManager.Instance.KillDiaglogue();
        }
    }
