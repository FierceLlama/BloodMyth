using UnityEngine;
using System.Collections;

public class Resting : MonoBehaviour
{
    public GameObject player;
    private Player _playerScript;

    void Start()
        {
        this._playerScript = this.player.GetComponent<Player>();
        }

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this._playerScript.Resting();
        }
    }
}