using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLeaf : MonoBehaviour
    {
    public GameObject player;
    private Player _playerScript;

    private void Start()
        {
        this._playerScript = this.player.GetComponent<Player>();
        }

    private void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._playerScript.addLeaf();
            this.gameObject.SetActive(false);
            }
        }
    }