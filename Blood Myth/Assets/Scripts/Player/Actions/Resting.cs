using UnityEngine;
using System.Collections;

public class Resting : MonoBehaviour
{
    public GameObject player;

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Resting();
        }
    }

    void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerManager>().CheckPlayerFatigue();
        }
    }
}