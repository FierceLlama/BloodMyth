using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueMonsterAttack : MonoBehaviour
    {
    public GameObject player;
    private Player _playerScript;
    private FatigueMonster _fatMon;
    
    void Start()
        {
        this._playerScript = player.GetComponent<Player>();
        this._fatMon = this.GetComponent<FatigueMonster>();
        }

    private void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._fatMon.skeletonAnimation.state.SetAnimation(0, "Attack", false);
            this._fatMon.skeletonAnimation.state.AddAnimation(0, "Idle", true, 1.0f);
            this._playerScript.FatigueHazard();
            if (!this._playerScript.CheckCrisis())
                {
                StartCoroutine("AttackSequence");
                }
            }
        }

    IEnumerator AttackSequence()
        {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
        }
    }