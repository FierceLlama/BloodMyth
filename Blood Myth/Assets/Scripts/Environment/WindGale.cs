using UnityEngine;
using System.Collections;

public class WindGale : MonoBehaviour
{
    public GameObject player;

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().InCold();
        }
    }
}