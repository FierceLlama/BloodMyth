using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueHazard : MonoBehaviour
{
    public GameObject player;
    public float timeDelay = 2.0f;
    private float _currentTime = 0.0f;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            //player.GetComponent<Player>().FatigueHazard();
        }
    }

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this._currentTime += Time.deltaTime;
            if (this._currentTime >= this.timeDelay)
            {
                //player.GetComponent<Player>().FatigueHazard();
                this._currentTime = 0.0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this._currentTime = 0.0f;
        }
    }
}