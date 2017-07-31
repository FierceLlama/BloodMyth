using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerFatMon : MonoBehaviour
    {
    private static bool doneThisShitAlready = false;
    public GameObject start;
    public static bool completed = false;
    public GameObject player;
    private Player _playerScript;
    private FatigueMonster _fatMon;
    public GameObject leaf;

    private void Start()
        {
        if (doneThisShitAlready)
            {
            this.gameObject.SetActive(false);
            }
        else if(completed)
            {
            this._fatMon = this.GetComponentInChildren<FatigueMonster>();
            this._playerScript = this.player.GetComponent<Player>();
            }
        else
            {
            this._fatMon = this.GetComponentInChildren<FatigueMonster>();
            this.start.transform.position = this.player.transform.position;
            this._playerScript = this.player.GetComponent<Player>();
            this.leaf.SetActive(false);
            }
        }

    public void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if(inPlayer.gameObject.tag == "Player")
            {
            this._fatMon.skeletonAnimation.state.SetAnimation(0, "Attack", false);
            this._fatMon.skeletonAnimation.state.AddAnimation(0, "Idle", true, 1.0f);
            if (!completed)
                {
                completed = true;
                this._playerScript.fatigueState = this._playerScript.playerDeath;
                this._playerScript.playerDeath.Enter();
                StartCoroutine("WaitForDeath");
                }
            else
                {
                doneThisShitAlready = true;
                this.player.GetComponent<Player>().FatigueHazard();
                StartCoroutine("AttackSequence");
                }
            }
        }
    public void KillDialogue()
        {
        DialogueManager.Instance.KillDiaglogue();
        }

    IEnumerator WaitForDeath()
        {
        yield return new WaitForSeconds(3.0f);
        AudioManager.Instance.StopSound("Summer",AudioType.Music);
        GameManager.Instance.GetComponent<BM_SceneManager>().ResetScene();
        }

    IEnumerator AttackSequence()
        {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
        }
    }