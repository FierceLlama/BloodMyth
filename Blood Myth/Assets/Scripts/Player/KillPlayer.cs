using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
    {
    public GameObject player;
    private Player _playerScript;

    void Start()
        {
        this._playerScript = this.player.GetComponent<Player>();
        }

    private void OnTriggerEnter2D(Collider2D inPlayer)
        {
        this._playerScript.PlayerDeathSequence();
        }
    }