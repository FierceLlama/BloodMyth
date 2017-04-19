using UnityEngine;
using System.Collections;

public class TotemBase : MonoBehaviour
{
    public Color totemColor;
    private GameObject _player;

    void Start()
    {
        this._player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //this._player.GetComponent<Player>().addTotemPowers();
            ParticleSystem.MainModule main = this._player.GetComponent<TotemPower>().totemHalo.main;
            main.startColor = totemColor;
        }
    }
}