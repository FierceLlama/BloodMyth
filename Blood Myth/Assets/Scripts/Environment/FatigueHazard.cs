using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueHazard : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().FatigueHazard();
        }
    }
}